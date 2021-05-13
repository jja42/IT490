package_name=$1
version_num=$2
validate=$3

password="deployment"
target="it490-deployment"

sshpass -p $password ssh $target@25.5.217.132 echo "[$package_name]"| tee /home/$target/git/IT490/Deployment/deploy.ini
