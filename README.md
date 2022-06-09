# Backstage Templates

This project contains the backstage templates which allow developers to create and scaffold new services, packages and other applications.

## Dotnet package
Allows developers to create and scaffold a new .Net and .Net core Nuget package. 
The scaffolded package contains:
- docs/: Contains the documentation of the package
- src/: Contains the source of the package. By default it will create two projects which will create two packages, {PackageName}.nupgk and {PackageName}.Abstractions.nupgk.
- tests/: Contains the tests for the code.
- azure-pipelines-docs.yml: Contains the azdevops pipeline for building the documentation.
- azure-pipelines.yml: Contains the azdevops pipeline for building and publishing the package.
- catalog-info.yml: Contains the information of the package, so it is registered in the backstage catalog.
- mkdcos.yml: Contains the configuration for generating the docs.

The template.yml file contains the configuration for the backstage templating.
What the template does is:
- In the paremters section we define the input parameters and the validation rules for each of them. For validation it uses the following library [react-jsonschema-form](https://react-jsonschema-form.readthedocs.io/en/latest/). The ownerpicker widget is a custom input field which allows users to select an owner based on the registered users and groups.
- Below that there's the steps section which defines which steps the scaffolding process will use:
    - The fetch-base creates the scaffolded code by using cookie cutter. First it gets the templating code and replaces the parameters with the input values. 
    - The publish step creates a github repository and pushes the scaffolded code.
    - Registers the catalog in the backstage catalog registry.
    - Creates the azure devops pipelines. This is the custom created action.
    
- The last section is the output which allows outputing some values to the user in the UI.

 For more information you can refer to the backstage documentation: [Writing templates](https://backstage.io/docs/features/software-templates/writing-templates)

