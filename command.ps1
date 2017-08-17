# Touch Integration Kit
# pichit@sripirom.com

$nugetRepository = "https://api.nuget.org/v3/index.jso"
$solutionFile = "./src/Tik.Web.sln"
$myappAppname = "Tik.Web.OwinNancy"
function Func_Build {

    Write-Host "restore packages $solutionFile"
    & nuget restore $solutionFile -Source $nugetRepository  |Out-Host
    if (! $?) { throw "nuget restore failed" }

    Write-Host "build $solutionFile"
    & msbuild $solutionFile /t:rebuild /p:Configuration=Debug /fl |Out-Host
    if (! $?) { throw "msbuild failed" }

    Write-Output "build success."
}

function Func_Publish
{
    Write-Host "build $solutionFile"
    & msbuild $solutionFile /t:rebuild /p:Configuration=Release /fl |Out-Host
    if (! $?) { throw "msbuild failed" }

    Write-Output "build success."
}

function Func_Start
{
    Write-Output "Start app $myappAppname"
    & mono "./src/$myappAppname/bin/Debug/$myappAppname.exe"
}

function Func_StartLocal
{
    $env:HostAddress = "localost"
    $env:HostPort = "8001"
    Write-Output "Start app $myappAppname"
    & mono "./src/$myappAppname/bin/Debug/$myappAppname.exe"
}

function Func_BuildDocker
{
    & docker build -t tik-owinnancy .
}

function Func_RunDocker
{
    & docker run -d -p 8001:8001 e- HostAddress=* e- HostPort=8001 tik-owinnancy:latest
}


$argcase = $args[0]
Write-Output "call function $argcase"
switch ($argcase) 
    { 
        "Build" {Func_Build} 
        "Start" {Func_Start} 
        "StartLocal" {Func_StartLocal} 
        "Publish" {Func_Publish} 
        "BuildDocker" {Func_BuildDocker} 
        "RunDocker" {Func_RunDocker} 
        "ClearDocker" {Func_ClearDocker} 
        default {"The argument command could not be determined."}
    }

