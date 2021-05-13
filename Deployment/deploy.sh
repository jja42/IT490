#!/bin/bash

#Get vars from arguments

package_dest=$3
package_name=$1
version_num=$2
package_path="Packages/$package_name"_"$version_num.tgz"

#determine ip to send to
case $package_dest in

	FE_QA)
		target="alexxander"
		ip_addr="25.70.254.189"
		;;
	DB_QA)
		target="diogocardoso"
		ip_addr="25.88.192.231"
		password="@Ubunutpass"
		;;
	API_PD)
		target="it490"
                ip_addr="25.7.135.98"
		password="4947"
                ;;
	FE_PD)
		target="alexander"
                ip_addr="25.70.254.189"
                ;;
	DB_PD)
		target="diogocardoso"
                ip_addr="25.65.7.88"
		password="@Ubuntupass"
                ;;
	API_QA)
		target="it490-vm-3"
                ip_addr="25.72.197.15"
		password="4947"
                ;;
	TEST)
		target="joao-dev"
		ip_addr="25.89.104.232"
		password="it490dev"
		;;
	*)
		echo "Invalid Target Location"
		;;
esac

echo sending $package_path to $target @ $ip_addr;

sshpass -p $password scp $package_path $target@$ip_addr:~/git/IT490/Packages

sshpass -p $password ssh $target@$ip_addr tar -xf /home/$target/git/IT490/$package_path

sshpass -p $password ssh $target@$ip_addr mkdir /home/$target/git/IT490/$package_name-$version_num

sshpass -p $password ssh $target@$ip_addr mv files/ /home/$target/git/IT490/$package_name-$version_num

sshpass -p $password ssh $target@$ip_addr  /home/$target/git/IT490/$package_name-$version_num/files/install/install.sh /home/$target/git/IT490/$package_name-$version_num/files/

#source <(grep dest deploy.ini)
#echo $dest
