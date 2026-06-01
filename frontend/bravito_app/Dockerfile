# Estágio 1: Build do Flutter Web
FROM ghcr.io/cirruslabs/flutter:stable AS build

WORKDIR /app

# Copia os arquivos do projeto para o container
COPY . .

# Instala as dependências e gera os assets nativos
RUN flutter pub get
RUN dart run flutter_launcher_icons
RUN flutter build web --release

# Estágio 2: Servidor Web (Nginx)
FROM nginx:alpine

# Configuração do Nginx para suportar o roteamento do Flutter (SPA)
RUN echo $'server {\\n\\
    listen 80;\\n\\
    location / {\\n\\
        root /usr/share/nginx/html;\\n\\
        index index.html index.htm;\\n\\
        try_files $uri $uri/ /index.html;\\n\\
    }\\n\\
}' > /etc/nginx/conf.d/default.conf

# Copia os arquivos gerados no estágio de build para a pasta do Nginx
COPY --from=build /app/build/web /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
