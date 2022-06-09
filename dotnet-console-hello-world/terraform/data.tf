data "azurerm_key_vault" "vault" {
  name                = local.kv-general-name
  resource_group_name = local.kv_resource_group_name
}

data "azurerm_subscription" "current" {}

data "terraform_remote_state" "kubernetes" {
  backend = "azurerm"
  config = {
    subscription_id      = data.azurerm_subscription.current.subscription_id
    resource_group_name  = "rg-tfstate-01"
    storage_account_name = "fundatfstate${var.environment}"
    container_name       = "tfstate"
    key                  = "tfstate.kubernetes"
  }
}