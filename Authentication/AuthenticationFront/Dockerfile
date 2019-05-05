FROM node

RUN mkdir /usr/src/app
WORKDIR /usr/src/app

ENV PATH /usr/src/app/node_modules/.bin:$PATH

COPY ./AuthenticationFront/package.json package.json
RUN npm install
RUN npm install -g @angular/cli

COPY ./AuthenticationFront /usr/src/app

EXPOSE 4200

ENTRYPOINT ["ng", "serve", "--host", "0.0.0.0"]
