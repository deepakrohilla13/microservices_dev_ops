variable "ami_for_docker_rancher" { 
    description = "ami for docker and rancher to be installed"
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

variable "microuser_availability_zone1" { 
    description = "Default availablity zone for us-virginia zone us-east-1"
}

variable "microuser_key_name" { 
    description = "Default keyname for the user"
}

variable "microuser_availability_zone2" { 
    description = "Second availablity zone within reson us-east-1"
}