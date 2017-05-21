# Octobot
Automation for Octopus deploy

## Usage
```octobot create infrastructure/octopus.yaml```
This creates an octopus project based on settings in /infrastructure/octopus.yaml

### Octopus.yaml

```
  Project:
    Name: 'Microservice A`
    Lifecycle: `Default Lifecycle`
    Steps:
      - Comply:
        Type: Manual Intervention
        Name: Comply with Sox
        OnlyInEnvironment: PRE, PROD
        Setting2: ...
      - DeployNewASG:
        Type: AWS - Create Cloud Formation Stack
        Name: Deploy to new ASG
      - DeployPackage:
        Type: Deploy
        Name: Deploy Package
      - ManualTest:
        Type: Manual Intervention
        Name: Validate Deployment
        OnlyInEnvironment: PRE, PROD
      - Notify:
        Type: Slack
        Name: Notify Deploy Channel
        OnlyInEnvironment: PRE, PROD
    Variables:
      MicroserviceBurlDev:
        Name: MicroserviceB-Url
        Value: https://dev-aws-alb.test.com/serviceb
        Scope: DEV
      MicroserviceBurlTest:
        Name: MicroserviceB-Url
        Value: https://test-aws-alb.test.com/serviceb
        Scope: TEST
```
        
