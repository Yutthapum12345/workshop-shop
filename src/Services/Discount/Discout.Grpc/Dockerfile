FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

ENV ASPNETCORE_URLS=http://+:8080;https://+8081;

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Services/Discount/Discout.Grpc/Discout.Grpc.csproj", "src/Services/Discount/Discout.Grpc/"]
RUN dotnet restore "src/Services/Discount/Discout.Grpc/Discout.Grpc.csproj"
COPY . .
WORKDIR "/src/src/Services/Discount/Discout.Grpc"
RUN dotnet build "Discout.Grpc.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Discout.Grpc.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discout.Grpc.dll"]
