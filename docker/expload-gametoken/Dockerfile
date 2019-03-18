# It gives csc compiler of 2.7.0.62620 (e873e693) version
# FROM mono:5.14

FROM microsoft/dotnet:2.1-sdk

WORKDIR /root

ADD http://www.random.org/strings/?num=10&len=8&digits=on&upperalpha=on&loweralpha=on&unique=on&format=plain&rnd=new uuid
COPY XGold.cs /root/
COPY XGold.csproj /root/

RUN dotnet build -c Release

RUN mv bin/Release/netstandard2.0/Expload.XGold.dll /root/ && \
    mv bin/Release/netstandard2.0/Expload.XGold.pdb /root/ && \
    rm -r .bashrc .dotnet .local .nuget .profile bin obj

# After build finished the /root folder will contain only four files:
# Expload.XGold.dll, Expload.XGold.pdb
# XGold.cs, XGold.csproj
