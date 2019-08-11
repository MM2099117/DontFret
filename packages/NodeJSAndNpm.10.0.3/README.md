# NodeJSAndNpm

Nuget package includes:
- Node.js v8.12.0
- Npm v6.4.1

## Usage inside of csproj
```
<PropertyGroup>
  <BuildDependsOn>
    NpmInstall;
    GrunAll;
    $(BuildDependsOn)
  </BuildDependsOn>
</PropertyGroup>

<Target Name="NpmInstall" BeforeTargets="BeforeBuild; BeforeClean">
  <Message Text="Installing packages"/>
  <Exec Command="$(PkgNodeJSAndNpm)\npm install --scripts-prepend-node-path" WorkingDirectory="./" />
  <Message Text="Packages are installed."/>
 </Target>
  
<Target Name="GrunAll" BeforeTargets="BeforeBuild;BeforeClean">
  <Message Text="Running grunt all"/>
  <Exec Command="$(PkgNodeJSAndNpm)\node.exe node_modules\grunt\bin\grunt all" WorkingDirectory="./" />
  <Message Text="Grunt finished."/>
</Target>
```

❤️ https://nodejs.org/ | [Licence](https://github.com/nodejs/node/blob/master/LICENSE)

❤️ https://www.npmjs.com/ | [Licence](https://github.com/npm/cli/blob/latest/LICENSE)
