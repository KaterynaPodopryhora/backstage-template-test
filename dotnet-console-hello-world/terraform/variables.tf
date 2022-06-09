variable "subscription_id" {
  type = string
}

variable "default_description" {
  default = "Managed by Terraform"
}

variable "azure_region" {
  type = string
}

variable "environment" {
  type = string
}

variable "azure_ad_tenant_id" {
  type    = string
  default = "3a40f900-9165-459e-9aea-7eaf0d933d38"
}

variable "appname" {
  type    = string
  default = "{{cookiecutter.name}}-funda-io"
}

locals {
  tags = {
    Owner = "{{cookiecutter.owner}}"
    Team  = "{{cookiecutter.owner}}"
    App   = "{{cookiecutter.name}}-funda-io"
    env   = var.environment
  }
  kv-general-name        = "kvfregeneral${var.environment}"
  kv_resource_group_name = "rg-keyvault"
}
