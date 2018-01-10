resource "aws_vpc" "vpc-virginia" {
    cidr_block           = "10.0.0.0/16"
    enable_dns_hostnames = true
    enable_dns_support   = true
    instance_tenancy     = "default"

    tags {
        "Name" = "vpc-virginia"
    }
}

