# Demo horizontal pod autoscaling in Kubernetes using KEDA
A producer publish messages to queue in RabbitMQ and KEDA helps scale up multiple consumers to process the messages 

### Before getting started
* Kubernetes cluster
* helm3
* KEDA

##### Below apps need to be installed in Kubernetes cluster
* RabbitMQ
* Seq
* Kubeapps 

##### Proxy Kubernetes and related apps
```
# K8s Dashboard
kubectl proxy

# Kubeapps Dashboard
kubectl port-forward --namespace kubeapps service/kubeapps 8003:80

# Seq
kubectl port-forward --namespace dev svc/my-seq 8002:80 5341:5341

# RabbitMQ
kubectl port-forward -n dev svc/rabbitmq 15672:15672 5672:5672
```
### Let's go!!!
##### Build Consumer Docker image
```
docker build -t my-consumer:0.0.1 .
```
##### Run Helm scripts in WSL2 terminal
Open Ubuntu terminal, then
```
cd /mnt/g/Work/MyGitHub/DemoAutoscaling/
```
Run Helm install in Debug Mode
```
helm secrets install -n dev my-consumer ./charts/my-consumer/ \
-f ./charts/my-consumer/helm_vars/secrets.yaml \
-f ./charts/my-consumer/values.yaml \
--set fullnameOverride=my-consumer --dry-run --debug
```
Install MyConsumer app
```
helm secrets install -n dev my-consumer ./charts/my-consumer/ \
-f ./charts/my-consumer/helm_vars/secrets.yaml \
-f ./charts/my-consumer/values.yaml --debug
```
Upgrade MyConsumer Helm chart
```
helm secrets upgrade  -n dev my-consumer ./charts/my-consumer/ \
-f ./charts/my-consumer/helm_vars/secrets.yaml \
-f ./charts/my-consumer/values.yaml \
-f ./charts/my-consumer/override.yaml --debug
```
Uninstall MyConsumer app
```
helm uninstall my-consumer -n dev
```
