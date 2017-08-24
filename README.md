# Nancy-Owin-SignalR V 0.1

### Touch Integration Kit
### pichit@sripirom.com



install unget packages
- nuget install Microsoft.Owin.Hosting -Version 3.1.0
- nuget install Microsoft.Owin.Host.HttpListener -Version 3.1.0
- nuget install Microsoft.Owin.Diagnostics -Version 3.1.0
- nuget install Topshelf.Log4Net -Version 4.0.3
- nuget install Topshelf.Linux -Version 1.0.16.15
- nuget install Newtonsoft.Json -Version 6.0,8
- nuget install Nancy.Owin -Version 1.4.1
- nuget install Nancy.Serialization.JsonNet -Version 1.4.1


### envelopment variables
- HostAddress
- HostPort

- Commit hook tiga

- Add link Tiga



# Import the public repository GPG keys
curl http://packages.microsoft.com/keys/microsoft.asc | apt-key add -

# Register the Microsoft Ubuntu repository
curl http://packages.microsoft.com/config/ubuntu/14.04/prod.list | tee /etc/apt/sources.list.d/microsoft.list

# Update apt-get
sudo apt-get update

# Install PowerShell
sudo apt-get install -y powershell

# Start PowerShell
powershell

curl http://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg

sh -c 'echo "deb [arch=amd64] http://packages.microsoft.com/repos/microsoft-ubuntu-trusty-prod trusty main" > /etc/apt/sources.list.d/dotnetdev.list'

sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
echo "deb http://download.mono-project.com/repo/ubuntu trusty main" | sudo tee /etc/apt/sources.list.d/mono-official.list
sudo apt-get update