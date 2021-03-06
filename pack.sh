#!/bin/bash

#setup path for package
package_name=$1
version_num=$2
package_path="packages/$package_name/$package_name-$version_num"
file_path=$3

#check if version exists already
if  [ -d "$package_path" ]; then
	echo "That Version Already Exists"
	exit
fi

#create directory for package
mkdir -p $package_path

#copy files to the directory
cp -r $file_path $package_path

#setup package and zip it
package=$package_name'_'$version_num.tgz
tar -zcvf $package_path'/'$package -C "$package_path/$file_path" .

#copy package to deployment server
scp $package_path'/'$package it490-deployment@25.5.217.132:~/git/IT490/Deployment/Packages

#cleanup
rm -r "$package_path/$file_path"
