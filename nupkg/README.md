# Silkypackaging script

## parameter

| parameter名 | Is it required | Remark |
|:----|:-----|:-----|
| -repo | no | nugetWarehouse Address |
| -push | no | 是no将surgingcomponents are pushed tonugetstorehouse,Default is`false` |
| -apikey | no | nugetstorehouseapikey,if set`-push $true`,must provide`-apikey`value |
| -build | no | 是no构建surgingcomponent package,Default is`true` |

## pushnugetstorehouse
```
.\pack.ps1 -push $true -apikey "$apikey"
```