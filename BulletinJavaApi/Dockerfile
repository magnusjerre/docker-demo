FROM gradle:7.6.1-jdk17 as build-stage
WORKDIR /home/gradle/project
COPY . ./
RUN gradle bootJar --no-daemon
# https://codefresh.io/docs/docs/example-catalog/ci-examples/gradle/
# https://hub.docker.com/_/eclipse-temurin

FROM eclipse-temurin:17 as run-stage
RUN mkdir /opt/app
COPY --from=build-stage /home/gradle/project/build/libs/ /opt/app
EXPOSE 8080
CMD ["java", "-jar", "/opt/app/bulletin-0.0.1-SNAPSHOT.jar"]
