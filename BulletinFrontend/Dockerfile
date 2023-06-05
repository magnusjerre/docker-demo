FROM node:alpine as build-stage
RUN mkdir /myapp 
WORKDIR /myapp
COPY . .
RUN ["npm", "install"]
RUN ["npm", "run", "build"]

# Følgende to bygg-stadier vil gjøre at fremtidige bygg, der avhengigheter i package.json er uendret blir raskere.
# FROM node:alpine as node_modules_base
# RUN mkdir /myapp
# WORKDIR /myapp
# COPY ./package.json .
# RUN ["npm", "install"]

# FROM node_modules_base as build-stage
# # samme som for node_modules_base
# WORKDIR /myapp
# COPY . .
# RUN ["npm", "run", "build"]

FROM nginx:alpine
COPY --from=build-stage /myapp/dist /usr/share/nginx/html