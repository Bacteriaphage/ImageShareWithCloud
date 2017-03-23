<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="ImageSharingWithCloudService" generation="1" functional="0" release="0" Id="1c0f5d92-2c46-4fe4-8aed-51d9e8c56a93" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="ImageSharingWithCloudServiceGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="ImageSharingWebRole:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/LB:ImageSharingWebRole:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="ImageSharingWebRole:APPINSIGHTS_INSTRUMENTATIONKEY" defaultValue="">
          <maps>
            <mapMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/MapImageSharingWebRole:APPINSIGHTS_INSTRUMENTATIONKEY" />
          </maps>
        </aCS>
        <aCS name="ImageSharingWebRole:AzureStorageString" defaultValue="">
          <maps>
            <mapMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/MapImageSharingWebRole:AzureStorageString" />
          </maps>
        </aCS>
        <aCS name="ImageSharingWebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/MapImageSharingWebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="ImageSharingWebRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/MapImageSharingWebRoleInstances" />
          </maps>
        </aCS>
        <aCS name="ImageSharingWorkerRole:APPINSIGHTS_INSTRUMENTATIONKEY" defaultValue="">
          <maps>
            <mapMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/MapImageSharingWorkerRole:APPINSIGHTS_INSTRUMENTATIONKEY" />
          </maps>
        </aCS>
        <aCS name="ImageSharingWorkerRole:AzureSQLDatabase" defaultValue="">
          <maps>
            <mapMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/MapImageSharingWorkerRole:AzureSQLDatabase" />
          </maps>
        </aCS>
        <aCS name="ImageSharingWorkerRole:AzureStorageString" defaultValue="">
          <maps>
            <mapMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/MapImageSharingWorkerRole:AzureStorageString" />
          </maps>
        </aCS>
        <aCS name="ImageSharingWorkerRole:Microsoft.ServiceBus.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/MapImageSharingWorkerRole:Microsoft.ServiceBus.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="ImageSharingWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/MapImageSharingWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="ImageSharingWorkerRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/MapImageSharingWorkerRoleInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:ImageSharingWebRole:Endpoint1">
          <toPorts>
            <inPortMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/ImageSharingWebRole/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapImageSharingWebRole:APPINSIGHTS_INSTRUMENTATIONKEY" kind="Identity">
          <setting>
            <aCSMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/ImageSharingWebRole/APPINSIGHTS_INSTRUMENTATIONKEY" />
          </setting>
        </map>
        <map name="MapImageSharingWebRole:AzureStorageString" kind="Identity">
          <setting>
            <aCSMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/ImageSharingWebRole/AzureStorageString" />
          </setting>
        </map>
        <map name="MapImageSharingWebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/ImageSharingWebRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapImageSharingWebRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/ImageSharingWebRoleInstances" />
          </setting>
        </map>
        <map name="MapImageSharingWorkerRole:APPINSIGHTS_INSTRUMENTATIONKEY" kind="Identity">
          <setting>
            <aCSMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/ImageSharingWorkerRole/APPINSIGHTS_INSTRUMENTATIONKEY" />
          </setting>
        </map>
        <map name="MapImageSharingWorkerRole:AzureSQLDatabase" kind="Identity">
          <setting>
            <aCSMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/ImageSharingWorkerRole/AzureSQLDatabase" />
          </setting>
        </map>
        <map name="MapImageSharingWorkerRole:AzureStorageString" kind="Identity">
          <setting>
            <aCSMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/ImageSharingWorkerRole/AzureStorageString" />
          </setting>
        </map>
        <map name="MapImageSharingWorkerRole:Microsoft.ServiceBus.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/ImageSharingWorkerRole/Microsoft.ServiceBus.ConnectionString" />
          </setting>
        </map>
        <map name="MapImageSharingWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/ImageSharingWorkerRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapImageSharingWorkerRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/ImageSharingWorkerRoleInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="ImageSharingWebRole" generation="1" functional="0" release="0" software="D:\Course\CS526\ImageSharingWithCloudService\ImageSharingWithCloudService\csx\Debug\roles\ImageSharingWebRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="APPINSIGHTS_INSTRUMENTATIONKEY" defaultValue="" />
              <aCS name="AzureStorageString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;ImageSharingWebRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;ImageSharingWebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;ImageSharingWorkerRole&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/ImageSharingWebRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/ImageSharingWebRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/ImageSharingWebRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="ImageSharingWorkerRole" generation="1" functional="0" release="0" software="D:\Course\CS526\ImageSharingWithCloudService\ImageSharingWithCloudService\csx\Debug\roles\ImageSharingWorkerRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="APPINSIGHTS_INSTRUMENTATIONKEY" defaultValue="" />
              <aCS name="AzureSQLDatabase" defaultValue="" />
              <aCS name="AzureStorageString" defaultValue="" />
              <aCS name="Microsoft.ServiceBus.ConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;ImageSharingWorkerRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;ImageSharingWebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;ImageSharingWorkerRole&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/ImageSharingWorkerRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/ImageSharingWorkerRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/ImageSharingWorkerRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="ImageSharingWebRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="ImageSharingWorkerRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="ImageSharingWebRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="ImageSharingWorkerRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="ImageSharingWebRoleInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="ImageSharingWorkerRoleInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="a040a8a7-8c2a-4f89-a370-0f56064d6b24" ref="Microsoft.RedDog.Contract\ServiceContract\ImageSharingWithCloudServiceContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="91effb39-dcfc-4192-8d2d-294923999952" ref="Microsoft.RedDog.Contract\Interface\ImageSharingWebRole:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/ImageSharingWebRole:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>