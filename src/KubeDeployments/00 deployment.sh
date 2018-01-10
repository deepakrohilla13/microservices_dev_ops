#cd "E:\POC\microservices_dev_ops\src>"
#docker build -t prod/todo:latest .\TodoAPI\

# Create folder in vm /shared

docker built -t deepakrohilla13/todo:latest ../TodoAPI/.

docker push deepakrohilla13/todo:latest

minikube start --vm-driver=hyperv --hyperv-virtual-switch "NewEVirtualSwitch" --cpus=2 --memory=4096
