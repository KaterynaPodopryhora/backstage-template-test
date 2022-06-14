# This is a placeholder until the funda-specific Docker image can be integrated
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS builder

# Authenticate NuGet with the Azure Artifacts feed
RUN curl -L https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh  | sh
ARG NUGET_ACCESS_TOKEN
ARG NUGET_ENDPOINT
ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS "{\"endpointCredentials\": [{\"endpoint\":\"${NUGET_ENDPOINT}\", \"password\":\"${NUGET_ACCESS_TOKEN}\"}]}"

# Restore dependencies first (optimizes the build time by making use of Docker's layers)
ARG PROJECT="${{ values.applicationName }}"
COPY NuGet.config repo/
COPY src/$PROJECT/$PROJECT.csproj repo/src/$PROJECT/
RUN dotnet restore repo/src/$PROJECT/$PROJECT.csproj

# Builds the project and publishes the binaries
COPY . repo
RUN dotnet publish repo/src/$PROJECT/$PROJECT.csproj -c release -o /publish


FROM funda.azurecr.io/funda-aspnet:6.0-2.1.1

# Copy files from build stage
WORKDIR /app
COPY --from=builder /publish .

# Create a new user to run this app
RUN adduser --disabled-password --gecos "" appuser --force-badname --home /home/appuser --uid 5000 \
  && chown -R appuser /app
USER appuser

EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000
ENTRYPOINT ["dotnet", "${{ values.namespacePrefix }}.dll"]