# Campaign Monitor workflow for Umbraco Forms
A custom workflow to allow users to map Umbraco Forms to a Campaign Monitor list.

## Configuration:

```
<add key="umbFormCampApiKey" value="{Your API KEY}"/>
<add key="umbFormCampClientId" value="{Your Client ID"/>
```

Api Key and Client key can be found by login into your [Campaign Monitor](https://www.campaignmonitor.com/) account and going to:

 - Account settings
 - API Keys 

![Api Keys screen][screen]

**Version 1.0.0**

*Tested against*

Umbraco 7.6.6

Umbraco Forms 6.0.2

[screen]:./media/campagin-monitor-api-keys.jpg "Api Keys screen"
