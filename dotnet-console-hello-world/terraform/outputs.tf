
output "pod_identities" {
  value = {
    app = {
      id        = azurerm_user_assigned_identity.pod_identity.id,
      client_id = azurerm_user_assigned_identity.pod_identity.client_id
    }
  }
}