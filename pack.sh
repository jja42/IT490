#!/bin/bash

version_num="test_version"
package_path="packages/$version_num"
mkdir -p $package_path

for i; do
	cp -r $i $package_path
done

package='testPackage_version_'$version_num.tgz
tar -zcvf $package_path'/'$package $package_path

scp $package_path'/'$package joao-dev@25.89.104.232:~/Downloads

#rm -R $package_path
