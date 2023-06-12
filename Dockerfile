# Use the official .NET SDK image as a parent image to build the app.
FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env

# Set the working directory.
WORKDIR /app

# Copy the certificate into the build image.
COPY ./cert.pfx /https/aspnetapp.pfx

# Copy the csproj and restore as distinct layers.
COPY ./Application/*.csproj ./Application/
COPY ./Domain/*.csproj ./Domain/
COPY ./Infrastructure/*.csproj ./Infrastructure/
COPY ./Web/*.csproj ./Web/
RUN dotnet restore ./Web/

# Copy everything else and build.
COPY . .
RUN dotnet publish -c Release -o out ./Web/

# Use the ASP.NET runtime image to run the app.
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Copy the certificate from the build image to the runtime image.
COPY --from=build-env /https/aspnetapp.pfx /https/aspnetapp.pfx

# Convert the pfx file to a pem file, and then to a crt file.
RUN openssl pkcs12 -in /https/aspnetapp.pfx -out /usr/local/share/ca-certificates/aspnetapp.pem -nodes -password pass:YourPassword && \
    openssl x509 -outform der -in /usr/local/share/ca-certificates/aspnetapp.pem -out /usr/local/share/ca-certificates/aspnetapp.crt

# Update the trusted certificates.
RUN chmod 644 /usr/local/share/ca-certificates/aspnetapp.crt && update-ca-certificates

WORKDIR /app
COPY --from=build-env /app/out .

# Inform Docker that the container is listening on the specified port at runtime.
EXPOSE 80

# Run the specified command within the container.
CMD ["dotnet", "Web.dll"]
