###BUILD ANGULAR SOLUTION###

FROM node:16-alpine3.14 as builder
WORKDIR /usr/src/app

COPY package.json yarn.lock ./

RUN yarn global add npm@8.6.0
RUN yarn install

COPY . ./

ARG ENVIRONMENT

RUN yarn build --${ENVIRONMENT} --output-path=dist
#RUN npm run build --configuration ${ENVIRONMENT} --output-path=dist

###BUILD NGINX AND COPYING FILES###

FROM nginx:1.21.6-alpine

COPY nginx.conf /etc/nginx/nginx.conf

COPY --from=builder /usr/src/app/dist /usr/share/nginx/html

EXPOSE 4200

CMD ["nginx", "-g", "daemon off;"]