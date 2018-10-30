# It gives csc compiler of 2.7.0.62620 (e873e693) version
# FROM mono:5.14

FROM microsoft/dotnet:2.1-sdk

WORKDIR /root

ADD http://www.random.org/strings/?num=10&len=8&digits=on&upperalpha=on&loweralpha=on&unique=on&format=plain&rnd=new uuid
COPY GameToken.cs /root/
COPY GameToken.csproj /root/

RUN dotnet build -c Release

RUN mv bin/Release/netstandard2.0/Expload.GameToken.dll /root/ && \
    mv bin/Release/netstandard2.0/Expload.GameToken.pdb /root/ && \
    rm -r .bashrc .dotnet .local .nuget .profile bin obj

# After build finished the /root folder will contain only four files:
# Expload.GameToken.dll, Expload.GameToken.pdb 
# GameToken.cs, GameToken.csproj
