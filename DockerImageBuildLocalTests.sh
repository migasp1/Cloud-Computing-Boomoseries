cd boomseries-Netflix-api/
docker build -f ./boomoseries-Netflix-api/Dockerfile -t netflixmicroservice .
cd ..
cd boomosseries-GoodReads-api/
docker build -f ./boomosseries-GoodReads-api/Dockerfile -t goodreadsmicroservice .
cd ..
cd bomoseries-Series-api/
docker build -f ./bomoseries-Series-api/Dockerfile -t seriesmicroservice .
cd ..
cd bomoseries-Disney-api/
docker build -f ./bomoseries-Disney-api/Dockerfile -t disneymicroservice .
cd ..
cd boomoseries-Amazon-api/
docker build -f ./boomoseries-Amazon-api/Dockerfile -t amazonmicroservice .
cd ..
cd boomoseries-Books-api/
docker build -f ./boomoseries-Books-api/Dockerfile -t booksmicroservice .
cd ..
cd boomoseries-IMDB-api/
docker build -f ./boomoseries-IMDB-api/Dockerfile -t imdbmicroservice .
cd ..
cd boomoseries-Movies-api/
docker build -f ./boomoseries-Movies-api/Dockerfile -t moviesmicroservice .
cd ..
cd boomoseries-OrchAuth-api/
docker build -f ./boomoseries-OrchAuth-api/Dockerfile -t orchmicroservice .
cd ..
cd boomoseries-prefs-api/
docker build -f ./boomoseries-prefs-api/Dockerfile -t prefsmicroservice .
cd ..
cd boomoseries-Search-api/
docker build -f ./boomoseries-Search-api/Dockerfile -t searchmicroservice .
cd ..
cd boomoseries-Users-api/
docker build -f ./boomoseries-Users-api/Dockerfile -t usersmicroservice .
