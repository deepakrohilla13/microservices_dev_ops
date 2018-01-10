/* Micro Service provisinging with aws iam user microuser */

provider "aws" {
    access_key = "${var.microuser_access_key}" 
    secret_key = "${var.microuser_secret_key}" 
    region = "${var.aws_region}"
}

resource "aws_vpc" "vpc-virginia" {
    cidr_block           = "10.0.0.0/16"
    enable_dns_hostnames = true
    enable_dns_support   = true
    instance_tenancy     = "default"

    tags {
        "Name" = "vpc-virginia"
    }
}


resource "aws_internet_gateway" "igw-virginia" {
    vpc_id                  = "${aws_vpc.vpc-virginia.id}"

    tags {
        "Name" = "igw-virginia"
    }
}


resource "aws_subnet" "subnet-11712075-subnet-public-1b" {
    vpc_id                  = "${aws_vpc.vpc-virginia.id}"
    cidr_block              = "10.0.192.0/18"
    availability_zone       = "us-east-1b"
    map_public_ip_on_launch = true

    tags {
        "Name" = "subnet-public-1b"
    }
}

resource "aws_subnet" "subnet-a34c1dc7-subnet-public-1a" {
    vpc_id                  = "${aws_vpc.vpc-virginia.id}"
    cidr_block              = "10.0.128.0/18"
    availability_zone       = "us-east-1b"
    map_public_ip_on_launch = true

    tags {
        "Name" = "subnet-public-1a"
    }
}

resource "aws_subnet" "subnet-7d9d0c20-subnet-private-1a" {
    vpc_id                  = "${aws_vpc.vpc-virginia.id}"
    cidr_block              = "10.0.0.0/18"
    availability_zone       = "us-east-1a"
    map_public_ip_on_launch = false

    tags {
        "Name" = "subnet-private-1a"
    }
}

resource "aws_subnet" "subnet-51792835-subnet-private-1b" {
    vpc_id                  = "${aws_vpc.vpc-virginia.id}"
    cidr_block              = "10.0.64.0/18"
    availability_zone       = "us-east-1b"
    map_public_ip_on_launch = false

    tags {
        "Name" = "subnet-private-1b"
    }
}

resource "aws_eip" "eip_nat_gateway" {
}

resource "aws_nat_gateway" "nat_gateway" {
    allocation_id = "${aws_eip.eip_nat_gateway.id}"
    subnet_id = "${aws_subnet.subnet-a34c1dc7-subnet-public-1a.id}"
}



resource "aws_route_table" "public-RT" {
    vpc_id                  = "${aws_vpc.vpc-virginia.id}"

    route {
        cidr_block = "0.0.0.0/0"
        gateway_id = "${aws_internet_gateway.igw-virginia.id}"
    }

    tags {
        "Name" = "public-RT"
    }
}

resource "aws_route_table" "private-RT" {
    vpc_id                  = "${aws_vpc.vpc-virginia.id}"

    route {
        cidr_block = "0.0.0.0/0"
    }

    tags {
        "Name" = "private-RT"
    }
}



resource "aws_route_table_association" "public-RT-rtbassoc-78bb7f04" {
    route_table_id = "${aws_route_table.public-RT.id}"
    subnet_id = "${aws_subnet.subnet-11712075-subnet-public-1b.id}"
}

resource "aws_route_table_association" "public-RT-rtbassoc-91c307ed" {
    route_table_id = "${aws_route_table.public-RT.id}"
    subnet_id = "${aws_subnet.subnet-a34c1dc7-subnet-public-1a.id}"    
}

resource "aws_route_table_association" "private-RT-rtbassoc-eec40092" {
    route_table_id = "${aws_route_table.private-RT.id}"
    subnet_id = "${aws_subnet.subnet-7d9d0c20-subnet-private-1a.id}"        
}

resource "aws_route_table_association" "private-RT-rtbassoc-66cd091a" {
    route_table_id = "${aws_route_table.private-RT.id}"
    subnet_id = "${aws_subnet.subnet-51792835-subnet-private-1b.id}"            
}

resource "aws_security_group" "vpc-a81624d0-default" {
    name        = "default"
    description = "default VPC security group"
    vpc_id                  = "${aws_vpc.vpc-virginia.id}"

    ingress {
        from_port       = 0
        to_port         = 0
        protocol        = "-1"
        cidr_blocks     = ["0.0.0.0/0"]
    }


    egress {
        from_port       = 0
        to_port         = 0
        protocol        = "-1"
        cidr_blocks     = ["0.0.0.0/0"]
    }

}


resource "aws_efs_file_system" "shared_efs_for_containers" {
    performance_mode = "generalPurpose"
}