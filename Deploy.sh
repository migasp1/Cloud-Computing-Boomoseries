export PROJECT_ID=$(gcloud info --format='value(config.project)')
cd boomseries-Netflix-api/
docker build -f ./boomoseries-Netflix-api/Dockerfile -t eu.gcr.io/${PROJECT_ID}/netflixmicroservice .
cd ..
cd boomosseries-GoodReads-api/
docker build -f ./boomosseries-GoodReads-api/Dockerfile -t eu.gcr.io/${PROJECT_ID}/goodreadsmicroservice .
cd ..
cd bomoseries-Series-api/
docker build -f ./bomoseries-Series-api/Dockerfile -t eu.gcr.io/${PROJECT_ID}/seriesmicroservice .
cd ..
cd bomoseries-Disney-api/
docker build -f ./bomoseries-Disney-api/Dockerfile -t eu.gcr.io/${PROJECT_ID}/disneymicroservice .
cd ..
cd boomoseries-Amazon-api/
docker build -f ./boomoseries-Amazon-api/Dockerfile -t eu.gcr.io/${PROJECT_ID}/amazonmicroservice .
cd ..
cd boomoseries-Books-api/
docker build -f ./boomoseries-Books-api/Dockerfile -t eu.gcr.io/${PROJECT_ID}/booksmicroservice .
cd ..
cd boomoseries-IMDB-api/
docker build -f ./boomoseries-IMDB-api/Dockerfile -t eu.gcr.io/${PROJECT_ID}/imdbmicroservice .
cd ..
cd boomoseries-Movies-api/
docker build -f ./boomoseries-Movies-api/Dockerfile -t eu.gcr.io/${PROJECT_ID}/moviesmicroservice .
cd ..
cd boomoseries-OrchAuth-api/
docker build -f ./boomoseries-OrchAuth-api/Dockerfile -t eu.gcr.io/${PROJECT_ID}/orchmicroservice .
cd ..
cd boomoseries-prefs-api/
docker build -f ./boomoseries-prefs-api/Dockerfile -t eu.gcr.io/${PROJECT_ID}/prefsmicroservice .
cd ..
cd boomoseries-Search-api/
docker build -f ./boomoseries-Search-api/Dockerfile -t eu.gcr.io/${PROJECT_ID}/searchmicroservice .
cd ..
cd boomoseries-Users-api/
docker build -f ./boomoseries-Users-api/Dockerfile -t eu.gcr.io/${PROJECT_ID}/usersmicroservice .
docker images
#gcloud services enable containerregistry.googleapis.com
gcloud auth configure-docker
docker push eu.gcr.io/${PROJECT_ID}/seriesmicroservice
docker push eu.gcr.io/${PROJECT_ID}/disneymicroservice
docker push eu.gcr.io/${PROJECT_ID}/amazonmicroservice
docker push eu.gcr.io/${PROJECT_ID}/booksmicroservice
docker push eu.gcr.io/${PROJECT_ID}/imdbmicroservice
docker push eu.gcr.io/${PROJECT_ID}/moviesmicroservice
docker push eu.gcr.io/${PROJECT_ID}/orchmicroservice
docker push eu.gcr.io/${PROJECT_ID}/prefsmicroservice
docker push eu.gcr.io/${PROJECT_ID}/searchmicroservice
docker push eu.gcr.io/${PROJECT_ID}/usersmicroservice
docker push eu.gcr.io/${PROJECT_ID}/netflixmicroservice
docker push eu.gcr.io/${PROJECT_ID}/goodreadsmicroservice
docker images
gcloud container images list
kubectl get nodes
gcloud container clusters get-credentials boomoseries --zone europe-west1-b --project cn-boomoseries-353312