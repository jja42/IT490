#!/bin/bash

#setup path for package
package_name=$1
version_num=$2
package_path="packages/$package_name/$version_num"

#check if version exists already
if  [ -d "$package_path" ]; then
	echo "That Version Already Exists"
	exit
fi

#create directory for package and temp directory for files
mkdir -p "$package_path/files"

#parse args for files to be added and copy them to the temp directory
argc=$#
argv=("$@")
for ((i=2;i<$argc;i++)); do
	cp -r ${argv[$i]} "$package_path/files"
done

#setup package and zip it
package=$package_name'_'$version_num.tgz
tar -zcvf $package_path'/'$package $package_path

#copy package to deployment server
scp $package_path'/'$package it490-deployment@25.5.217.132:~/git/IT490/Deployment/Packages

#cleanup
rm -r "$package_path/files"
