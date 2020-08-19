# Helm charts
Helm chart repo with Helm secret to manage secrets (Ex: credentails...)

#### Run this script to check the output of yaml for Worker
```
helm secrets install ./worker/ \
-f ./worker/helm_vars/secrets.yaml \
-f ./worker/values.yaml \
--set fullnameOverride=my-worker --dry-run --debug --generate-name -n dev
```

#### Run this script to deploy new Worker
```
helm secrets install my-worker ./worker/ \
-f ./worker/helm_vars/secrets.yaml \
-f ./worker/values.yaml \
--set fullnameOverride=my-worker --debug -n dev
```

#### Run this script to see the new Worker chart
```
helm list -n dev
```

#### Run this script to uninstall Worker chart
```
helm uninstall my-worker -n dev
```
