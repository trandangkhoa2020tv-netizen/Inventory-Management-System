FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY QuanLyKhoHang.Api/QuanLyKhoHang.Api.csproj QuanLyKhoHang.Api/
COPY QuanLyKhoHang.Shared/QuanLyKhoHang.Shared.csproj QuanLyKhoHang.Shared/
RUN dotnet restore QuanLyKhoHang.Api/QuanLyKhoHang.Api.csproj

COPY QuanLyKhoHang.Api/ QuanLyKhoHang.Api/
COPY QuanLyKhoHang.Shared/ QuanLyKhoHang.Shared/
RUN dotnet publish QuanLyKhoHang.Api/QuanLyKhoHang.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

WORKDIR /app
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "QuanLyKhoHang.Api.dll"]

