variable "location" {
  description = "Detailed Azure region"
  default     = "eastus2"
}

variable "project_name" {
  description = "Project name prefix"
  default     = "arya-healthcare"
}

variable "environment" {
  description = "Environment (dev, staging, prod)"
  default     = "dev"
}
