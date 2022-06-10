{% raw %}
{{/* vim: set filetype=mustache: */}}
{{/*
Expand the name of the chart.
*/}}
{{- define "funda-helper.name" -}}
{{- $env_name := ternary "" .Values.env_name (eq (len (get .Values .Values.environment_name).envs) 1 ) -}}
{{- default (printf "%s-%s" .Chart.Name $env_name) .Values.nameOverride | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{/*
Create a default fully qualified app name.
We truncate at 63 chars because some Kubernetes name fields are limited to this (by the DNS naming spec).
If release name contains chart name it will be used as a full name.
*/}}
{{- define "funda-helper.fullname" -}}
{{- $env_name := ternary "" .Values.env_name (eq (len (get .Values .Values.environment_name).envs) 1 ) -}}
{{- if .Values.fullnameOverride -}}
{{- .Values.fullnameOverride | trunc 63 | trimSuffix "-" -}}
{{- else -}}
{{- $release_name := printf "%s-%s" .Release.Name (.Values.service_name | default "") | trimSuffix "-" -}}
{{- $name := default .Chart.Name .Values.nameOverride -}}
{{- if contains $name $release_name -}}
{{- printf "%s-%s" $release_name $env_name | trunc 63 | trimSuffix "-" -}}
{{- else -}}
{{- printf "%s-%s-%s" $release_name $name $env_name | trunc 63 | trimSuffix "-" -}}
{{- end -}}
{{- end -}}
{{- end -}}

{{- define "funda-helper.serviceName" -}}
{{- printf "%s-%s" .Release.Name (.Values.service_name | default "") | trimSuffix "-" -}}
{{- end -}}

{{- define "funda-helper.envname" -}}
{{- $env_name := ternary .Values.environment_name .Values.env_name (eq (len (get .Values .Values.environment_name).envs) 1 ) -}}
{{- printf "%s" $env_name -}}
{{- end -}}

{{/*
Create chart name and version as used by the chart label.
*/}}
{{- define "funda-helper.chart" -}}
{{- printf "%s-%s" .Chart.Name .Chart.Version | replace "+" "_" | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{/*
Create the name of the service account to use
*/}}
{{- define "funda-helper.serviceAccountName" -}}
{{- if .Values.serviceAccount.enabled -}}
    {{ default (include "funda-helper.fullname" .) .Values.serviceAccount.name }}
{{- else -}}
    {{ default "default" .Values.serviceAccount.name }}
{{- end -}}
{{- end -}}

{{- define "funda-helper.buildId" }}
{{- printf "%s-%s" .Values.buildkey (splitList "." .Values.image.tag | last) -}}
{{- end -}}
{% endraw %}