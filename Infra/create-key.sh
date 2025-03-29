#!/bin/bash

# Script to create Azure Key Vault and add secrets
# Save as: create-keyvault.sh

# Read MongoDB connection string from environment variable instead of hardcoding
# This requires you to set the environment variable before running the script
# Example: export MONGODB_CONNECTION_STRING="your-connection-string-here"

if [ -z "$MONGODB_CONNECTION_STRING" ]; then
  echo "Error: MONGODB_CONNECTION_STRING environment variable is not set."
  echo "Please set it using: export MONGODB_CONNECTION_STRING='your-connection-string'"
  exit 1
fi

# Create a resource group
echo "Creating resource group..."
az group create --name "LegoInventoryResourceGroup" --location "northeurope"

# Create a Key Vault
echo "Creating Key Vault..."
az keyvault create --name "LegoInventoryVault" --resource-group "LegoInventoryResourceGroup" --location "northeurope"

# Add the CosmosDB connection string to Key Vault
echo "Adding CosmosDB connection string to Key Vault..."
az keyvault secret set --vault-name "LegoInventoryVault" --name "MongoDB--ConnectionString" --value "$MONGODB_CONNECTION_STRING"

echo "Done! Azure Key Vault setup is complete."