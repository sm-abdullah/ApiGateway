# Getting Started with Rate Limit

This project build with asp.net core 5.0 contains following projects
- Api Gateway (Test Application)
- RateLimit (Actual module) 

### `Setting up Docker container`
you can use following commands in root direcotry.
```
docker-compose build
docker-compose up
```
you can run docker-compose up in root directory and it should be able to launch application 
on following URL [http://localhost:5000/testapi](http://localhost:5000/testapi) to view it in the browser.

### `Running on dev machine`
Simply open application in Visual Studio or rider by hitting play button should be able to launch application
on 5000 port.

### `Rate limit General Settings`
You can modified following settings in appsettings.json
```
 "RateLimitSettings": {
    "ClientIdHeader": "X-ClientId",
    "HttpStatusCode": 429,
    "QuotaExceededResponse": {
      "Content": "Rate limit exceeded. Try again in {0} seconds",
    }
  }
```
- ClientIdHeader -> Sepcify header name to read client id
- HttpStatusCode -> Specify response code when limit reached
- Content -> Specify default response where {0} is place holder to put remaining time


### `Setting up rate limit policies`

```
 "RateLimitPolicies": {
    "ClientRules": [
      {
        "clientId": "client01",
        "rules": [
          {
            "Endpoint": "*",
            "Period": "60s",
            "Limit": 10
          }
        ]
      },
      {
        "clientId": "*", // can be treated it as default policy
        "rules": [
          {
            "Endpoint": "*",
            "Period": "60s",
            "Limit": 10
          }
        ]
      },
    ]
  }
``` 
These above configuration are generic to consider future scaleablity. But at this moment RateLimit will just pick first rule against a client and apply period and limit on all request comming from same client. but it can be scaled up to have multiple endpoint policies by clients.
Note : if you do not specifiy header to identify client it will be pick default header and pic default policy.
