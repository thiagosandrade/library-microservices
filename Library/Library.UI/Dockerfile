###BUILD ANGULAR SOLUTION###

FROM node:alpine as builder
WORKDIR /usr/src/app

COPY package*.json ./

RUN npm install

COPY . .


ARG ENVIRONMENT

RUN npm run build -- --configuration $ENVIRONMENT --output-path=dist

###BUILD NGINX AND COPYING FILES###

FROM nginx:alpine

COPY nginx.conf /etc/nginx/nginx.conf

COPY --from=builder /usr/src/app/dist /usr/share/nginx/html

WORKDIR /usr/share/nginx/html

EXPOSE 4200