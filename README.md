# LinkBox


docker command example

```
docker run -d -p 8080:8080 --name linkbox --privileged -u root -e LANG=en_US.UTF-8 --restart=always -e TZ=Asia/Shanghai -v /etc/app/linkbox:/app/data -e PASSWORD=admin -d docker.io/8720826/linkbox
```
