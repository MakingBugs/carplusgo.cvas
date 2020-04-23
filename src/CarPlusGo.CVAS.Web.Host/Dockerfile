FROM 192.168.21.41/coresdk/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
ARG ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_ENVIRONMENT $ASPNETCORE_ENVIRONMENT
RUN env
RUN env |grep ASPNETCORE_ENVIRONMENT
ENV TZ=Asia/Shanghai
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

FROM 192.168.21.41/coresdk/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/CarPlusGo.CVAS.Web.Host/CarPlusGo.CVAS.Web.Host.csproj", "src/CarPlusGo.CVAS.Web.Host/"]
COPY ["src/CarPlusGo.CVAS.Web.Host/nuget.config", "src/CarPlusGo.CVAS.Web.Host/"]
COPY ["src/CarPlusGo.CVAS.Web.Core/CarPlusGo.CVAS.Web.Core.csproj", "src/CarPlusGo.CVAS.Web.Core/"]
COPY ["abp/Abp.AspNetCore/Abp.AspNetCore.csproj", "abp/Abp.AspNetCore/"]
COPY ["abp/Abp.Web.Common/Abp.Web.Common.csproj", "abp/Abp.Web.Common/"]
COPY ["abp/Abp/Abp.csproj", "abp/Abp/"]
COPY ["src/CarPlusGo.CVAS.MongoDBCore/CarPlusGo.CVAS.MongoDBCore.csproj", "src/CarPlusGo.CVAS.MongoDBCore/"]
COPY ["src/CarPlusGo.CVAS.Core/CarPlusGo.CVAS.Core.csproj", "src/CarPlusGo.CVAS.Core/"]
COPY ["abp/Abp.AutoMapper/Abp.AutoMapper.csproj", "abp/Abp.AutoMapper/"]
COPY ["abp/Abp.ZeroCore.EntityFrameworkCore/Abp.ZeroCore.EntityFrameworkCore.csproj", "abp/Abp.ZeroCore.EntityFrameworkCore/"]
COPY ["abp/Abp.ZeroCore/Abp.ZeroCore.csproj", "abp/Abp.ZeroCore/"]
COPY ["abp/Abp.Zero.Common/Abp.Zero.Common.csproj", "abp/Abp.Zero.Common/"]
COPY ["abp/Abp.EntityFrameworkCore/Abp.EntityFrameworkCore.csproj", "abp/Abp.EntityFrameworkCore/"]
COPY ["abp/Abp.EntityFramework.Common/Abp.EntityFramework.Common.csproj", "abp/Abp.EntityFramework.Common/"]
COPY ["abp/Abp.AspNetCore.SignalR/Abp.AspNetCore.SignalR.csproj", "abp/Abp.AspNetCore.SignalR/"]
COPY ["src/CarPlusGo.CVAS.Application/CarPlusGo.CVAS.Application.csproj", "src/CarPlusGo.CVAS.Application/"]
COPY ["src/CarPlusGo.CVAS.EntityFrameworkCore/CarPlusGo.CVAS.EntityFrameworkCore.csproj", "src/CarPlusGo.CVAS.EntityFrameworkCore/"]
COPY ["abp/Abp.Castle.Log4Net/Abp.Castle.Log4Net.csproj", "abp/Abp.Castle.Log4Net/"]
RUN dotnet restore "src/CarPlusGo.CVAS.Web.Host/CarPlusGo.CVAS.Web.Host.csproj" --configfile "src/CarPlusGo.CVAS.Web.Host/nuget.config"
COPY . .
WORKDIR "/src/src/CarPlusGo.CVAS.Web.Host"
RUN dotnet build "CarPlusGo.CVAS.Web.Host.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CarPlusGo.CVAS.Web.Host.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CarPlusGo.CVAS.Web.Host.dll"]