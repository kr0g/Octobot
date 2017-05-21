# Octobot
Automation for Octopus deploy

## Usage
`octobot init infrastructure/octopus.yaml`

This creates an octopus project based on settings in /infrastructure/octopus.yaml. Generally you do this only once.

`octobot variables infrastructure/octopus.yaml`

This runs octobot to modify the variables only, generally run by a sysadmin or development lead to update/override variables in higher (e.g. PRE, PROD) environments. 

*NOTE:* design consideration - very few variables are "sensative" like connection strings, token/api keys, etc. we need an easy way to allow sysadmin to keep those outside of version control but easily importable via octobot.

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
      MicroserviceBurl:
        Name: MicroserviceB-Url
        Type: Normal
          DEV: https://dev-aws-alb.test.com/serviceb
          TEST: https://test-aws-alb.test.com/serviceb
          PRE: https://pre-aws-alb.test.com/serviceb
          PROD: https://prod-aws-alb.test.com/serviceb
     MicroserviceBapiKey:
        Name: MicroserviceB-Api-Key
        Type: Sensitive
          DEV: DevIsOkayToHaveInGitRepoIguess
          TEST: REPLACED_BY_SYS_ADMIN
          PRE: REPLACED_BY_SYS_ADMIN
          PROD: REPLACED_BY_SYS_ADMIN
```
        
