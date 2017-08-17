# base
FROM tik-mono:latest
MAINTAINER sripirom


COPY ./src/Tik.Web.OwinNancy/bin/Release /app/owinnancy

EXPOSE 8001
CMD ["mono", "/app/owinnancy/Tik.Web.OwinNancy.exe"]