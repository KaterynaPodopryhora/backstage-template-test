terraform {
  backend "azurerm" {
    resource_group_name = "rg-tfstate-01"
    container_name      = "tfstate"
    key                 = "tfstate.{{cookiecutter.name}}-funda-io"
  }
}