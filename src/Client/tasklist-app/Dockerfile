# Stage 1: Build the React application
FROM node:16-alpine as build

# Set working directory
WORKDIR /app

# Add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# Install app dependencies
# A wildcard is used to ensure both package.json AND package-lock.json are copied
# where available (npm@5+)
COPY package.json ./
COPY package-lock.json ./

# Install dependencies
RUN npm install --silent
RUN npm install react-scripts@latest -g --silent

# Copy the react source code to work directory
COPY . ./

# Build the React application
RUN npm run build

# Stage 2: Serve the React application from Nginx
FROM nginx:1.21-alpine

# Copy the react build from Stage 1
COPY --from=build /app/build /usr/share/nginx/html

# Copy the default nginx.conf provided by create-react-app
COPY nginx.conf /etc/nginx/nginx.conf

# Expose port 80 to the outside once the container has launched
EXPOSE 80

# Containers run nginx with global directives and daemon off
ENTRYPOINT ["nginx", "-g", "daemon off;"]