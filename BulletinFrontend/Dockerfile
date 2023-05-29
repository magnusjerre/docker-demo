
# https://github.com/nodejs/docker-node/blob/main/README.md#how-to-use-this-image
# WORKDIR /usr/src/app
FROM node
WORKDIR /usr/src/app
COPY package.json .
RUN npm install
COPY . .
RUN npm run build

# 5173 for run build
EXPOSE 5173
CMD ["npm", "run", "host"]