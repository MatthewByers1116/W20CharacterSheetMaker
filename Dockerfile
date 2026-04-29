# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
# (Assuming your new project folder is named W20Builder)
COPY W20Builder/W20Builder.csproj W20Builder/
RUN dotnet restore W20Builder/W20Builder.csproj

# Copy the rest of the files and build
COPY . .
RUN dotnet publish W20Builder/W20Builder.csproj -c Release -o /app/publish

# Stage 2: Serve the static files using Nginx
FROM nginx:alpine
WORKDIR /usr/share/nginx/html

# Copy the published static files from the build stage
COPY --from=build /app/publish/wwwroot .

# Copy a custom nginx configuration to handle Blazor routing
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
