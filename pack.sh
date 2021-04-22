#!/bin/bash

package_name=$1
version_num=$2
package_path="packages/$package_name/$version_num"
mkdir $package_path
argc=$#
argv=("$@")
for ((i=2;i<$argc;i++)); do
	cp -r ${argv[$i]} $package_path
done

package=$package_name'_'$version_num.tgz
tar -zcvf $package_path'/'$package $package_path

scp $package_path'/'$package it490-deployment@25.5.217.132:~/Packages
