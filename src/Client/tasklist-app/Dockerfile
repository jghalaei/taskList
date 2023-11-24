FROM node:16.14.2
ENV NODE_ENV=production
WORKDIR /usr/src/app

# RUN chown -R node:node /usr/src/app
# USER node
COPY ["package.json", "package-lock.json", "./"]
RUN npm install 
COPY . .
EXPOSE 3000


RUN npm run build
CMD ["npm", "start"]
