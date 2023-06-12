# Use the official .NET SDK image as a parent image to build the app.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Set the working directory.
WORKDIR /app

# Copy the certificate into the build image.
COPY ./aspnetapp.pfx /https/aspnetapp.pfx

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

WORKDIR /app
COPY --from=build-env /app/out .

# Copy the certificate from the build image to the runtime image.
COPY --from=build-env /https/aspnetapp.pfx /https/aspnetapp.pfx

# Convert pfx certificate to crt
RUN openssl pkcs12 -in /https/aspnetapp.pfx -clcerts -nokeys -out /usr/local/share/ca-certificates/aspnetapp.crt -password pass:YourPassword

# Update ca-certificates
RUN update-ca-certificates

# Inform Docker that the container is listening on the specified port at runtime.
EXPOSE 80

# Run the specified command within the container.
CMD ["dotnet", "Web.dll"]
