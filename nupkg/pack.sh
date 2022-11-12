#!/usr/bin/env bash

set -euo pipefail

usage()
{
    cat <<END
pack.sh: linuxPackaging microservice component scripts in the environment
Parameters:
    -r | --repo <nuget repo>
       nuget Warehouse Address
    -p | --push <push>
       Whether to push components
    --ship-build <ship build>
       whether to skipbuild
    -k | --apikey <api key>
       nuget repo apikey
    -h | --help
       show help
END
}
nuget_repo=""
push=""
apikey=""
build="yes"
workdir=$(cd $(dirname $0); pwd)
srcPath="${workdir}/.."

while [[ $# -gt 0 ]]; do
  case "$1" in
    -r | --repo )
      echo "nuget repo is $2"
      nuget_repo="$2"; shift 2;;
    -p | --push )
       push="yes"; shift 1;;
    --ship-build )
       build=''; shift 1;;
    -k | --apikey )
       echo "apikey is $2"
       apikey="$2"; shift 2;;
    -h | --help )
        usage; exit 1 ;;
    *)
        echo "Unknown option $1"
        usage; exit 2 ;;
  esac
done

command -v dotnet >/dev/null 2>&1 || { echo >&2 "Please install firstdotcore sdk"; exit 1; }

function pack() {
  echo "this is ${packageName}"
  cd ${packagePath}
  rm -fr "${packagePath}/bin/Release"
  dotnet restore
  dotnet pack -c Release
  componentPath="${packagePath}/bin/Release/${packageName}.*.nupkg"
  mv ${componentPath} ${workdir}
}

components=(`cat ${workdir}/Components`)

if [[ $build ]]; then
  cd "${workdir}/.."
  for component in ${components[@]}
  do
    OLD_IFS="$IFS"
    IFS="/"
    component_name_arr=(${component})
    IFS="$OLD_IFS"
    if [[ "${component}" != "#*" ]]; then
      packageName=${component_name_arr[-1]}
      packagePath="${srcPath}/${component}"
      pack --packageName ${packageName} --packagepath ${packagePath}
    fi
  done
  cd ${workdir}
fi

if [[ $push ]]; then
   if [[ -z "$apikey" ]]; then
        echo "not setnugetwarehouseAPIKEY"
        exit 1
   fi
   pkgs=(`ls *.nupkg`)
   for pkg in "${pkgs[@]}"
   do
     dotnet nuget push ${pkg} -s $nuget_repo -k $apikey
   done
fi
