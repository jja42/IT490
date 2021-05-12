#!/bin/bash

#Get vars from arguments

package_dest=$3
package_name=$1
version_num=$2
package_path="Packages/$package_name"_"$version_num.tgz"

#determine ip to send to
case $package_dest in

	FE_QA)
		ip_addr="0.0.0.0"
		;;
	DB_QA)
		ip_addr="0.0.0.0"
		;;
	API_QA)
                ip_addr="0.0.0.0"
                ;;
	FE_PD)
                ip_addr="0.0.0.0"
                ;;
	DB_PD)
                ip_addr="0.0.0.0"
                ;;
	API_PD)
                ip_addr="0.0.0.0"
                ;;
	TEST)
		target="joao-dev"
		ip_addr="25.89.104.232"
		;;
	*)
		echo "Invalid Target Location"
		;;
esac

echo sending $package_path to $target @ $ip_addr;

sshpass -p "it490dev" scp $package_path $target@$ip_addr:~/git/IT490/Packages

ssh $target@$ip_addr "bash -s"i -- < /home/$target/git/IT490/unpack.sh /home/$target/git/IT490/Packages/$package_path

#source <(grep dest deploy.ini)
#echo $dest
