FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
RUN apt-get update && apt-get install -y locales
RUN sed -i -e 's/# en_US.UTF-8 UTF-8/en_US.UTF-8 UTF-8/' /etc/locale.gen && dpkg-reconfigure --frontend=noninteractive locales && update-locale LANG=en_US.UTF-8
ENV LANG en_US.UTF-8
RUN wget -qO- https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh | bash

FROM build AS publish
WORKDIR /app
ARG BuildConfig
COPY Resources.Api/bin/${BuildConfig}/net6.0/publish/ .

FROM base AS final
RUN apt-get update && apt-get install -y curl
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Resources.Api.dll"]