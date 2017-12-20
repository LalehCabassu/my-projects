#!/usr/bin/python

import sys

class NBIAS_data_metric:
    def __init__(self, fields_str):
        self.fields = {}
        self.parse(fields_str)

    def parse(self, str):
        labels = ['on_under', 'kind_hwy', 'levl_srvc', 'vclrinv', 'bypasslen', 'adttotal', 'adtyear',
            'aroadwidth', 'roadwidth', 'trafficdir', 'adtfuture', 'adtfutyear']

        self.full_bridge_id, rest_of_fields = str.split('\t', 1)
        self.full_bridge_id = self.full_bridge_id[1:]
        list_fields = rest_of_fields.split(',')
        for index in range(len(labels)):
            self.fields[labels[index]] = list_fields[index] if len(list_fields) > index else ''

    def toString(self):
        return self.full_bridge_id + '\t' + ','.join(self.fields.values())

class NBIAS_brinsp:

    def __init__(self, fields_str):
        self.fields = {}
        self.parse(fields_str)

    def parse(self, str):
        labels = ['district', 'county',  'funcclass', 'yearbuilt', 'lanes_on', 'lanes_under', 'designload', 'strflared',
            'railrating', 'transratin', 'arailratin', 'aendrating', 'histsign', 'oppostcl', 'servtypon', 'servtypund', 'material_main',
            'design_main', 'material_appr', 'design_appr', 'mainspans', 'appspans', 'skew', 'hclrinv', 'length', 'lftcurbsw', 'rtcurbsw',
            'deckwidth', 'vclrover', 'refvuc', 'vclrunder', 'refhuc', 'hclrurt', 'hclrult', 'dkrating', 'suprating', 'subrating',
            'chanprot', 'culvrating', 'orload', 'irload', 'strrating', 'deckgeom', 'underclr', 'posting', 'wateradeq', 'appralign',
            'propwork', 'inspdate', 'fraccrit', 'nbiimpcost', 'nbiyrcost', 'nstatecode', 'n_fhwa_reg', 'defhwy', 'paralstruc',
            'tempstruc', 'nhs_ind', 'yearrecon', 'dkstructyp', 'dksurftype', 'dkmembtype', 'dkprotect', 'truckpct', 'pierprot',
            'nbislen', 'nbi_rating', 'suff_prefx', 'suff_rate', 'bridgemed', 'climate_zone', 'isbridge',
            # 'bad_data'
            'yearlast', 'rangelast', 'fips_state', 'fhwa_regn', 'bridge_id', 'd_length', 'd_deckwidth', 'd_deckarea',
            # 'b_screened', 'f_mainspans', 'f_appspans'
            'state_brkey', 'state_reg'
            # 'traceflag'
            ]

        self.full_bridge_id, rest_of_fields = str.split('\t', 1)
        self.full_bridge_id = self.full_bridge_id[1:]
        list_fields = rest_of_fields.split(',')
        for index in range(len(labels)):
            self.fields[labels[index]] = list_fields[index] if len(list_fields) > index else ''

    def toString(self):
        result = ''
        for key in self.dic.keys():
            values = []
            for f in self.brinsp_columns:
                values.append(self.dic[key][f])
            result += 'b' + key + '\t' + ','.join(values) + '\n'

            for f in self.data_metric_req_columns:
                values.append(self.dic[key][f])
            result += 'dm' + key + '\t' + ','.join(values) + '\n'
        return result

def main(argv):
    nbias_data = open('data/1_reducer_brinsp_data_metric_output.txt', 'r')
    lines = nbias_data.readlines()
    mapper_max_raw_data_output = open('data/2_mapper_means_and_sigmas_brinsp_data_metric.txt', 'w')


    for line in lines:
    # for line in sys.stdin:
        code = line[:2]
        if code == 'dm':
            print line
        elif code == 'br':
            print line
            brinsp = NBIAS_brinsp(line)
            if float(brinsp.fields['mainspans']) > 0 and brinsp.fields['isbridge'] == 'B' and int(brinsp.fields['length']) > 0:



        data_raw = NBIAS_data_metric(line)
        # print data_raw.toString()
        mapper_max_raw_data_output.write(data_raw.toString() + '\n')

    nbias_data.close()
    mapper_max_raw_data_output.close()

if __name__ == '__main__':
    main(sys.argv)