resource "azurerm_user_assigned_identity" "pod_identity" {
  resource_group_name = data.terraform_remote_state.kubernetes.outputs.cluster_identities_rg
  location            = var.azure_region
  name                = "id-${var.appname}"

  tags = local.tags
}

resource "azurerm_key_vault_access_policy" "pod_vault_access" {
  key_vault_id = data.azurerm_key_vault.vault.id

  tenant_id = var.azure_ad_tenant_id
  object_id = azurerm_user_assigned_identity.pod_identity.principal_id

  key_permissions = [
    "get",
  ]

  secret_permissions = [
    "get",
  ]

  certificate_permissions = [
    "get",
  ]
}
