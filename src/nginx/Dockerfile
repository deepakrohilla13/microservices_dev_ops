FROM nginx

COPY index.html /usr/share/nginx/html

COPY nginx.conf /etc/nginx

COPY yl8bq8i0E_9GQ3HnWE6e1KllMwYobNHuPR4WlLhrpXU /usr/share/nginx/html/.well-known/acme-challenge/

COPY private.key /cert/private.key

COPY server.crt /cert/server.crt

COPY registry.password /etc/nginx/.htpasswd

RUN chmod 644 /usr/share/nginx/html/.well-known/acme-challenge/yl8bq8i0E_9GQ3HnWE6e1KllMwYobNHuPR4WlLhrpXU
