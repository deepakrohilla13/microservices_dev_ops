variable "ami_with_docker_rancher" { 
    description = "ami having docker and rancher pre installed"
}
 
variable "esb_5gb_volume" { 
    description = "Shared volume for containers on every host"
}

variable "microuser_access_key" {
    description = "for access key"
}

variable "microuser_secret_key" {
    description = "for secret"
}

variable "aws_region" {
    description = "region name for MSA setup"
}