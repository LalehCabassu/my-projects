#!/usr/bin/python

import sys
import re
from datetime import datetime

class NBIAS_data_metric_row:
    def __init__(self, fields_str):
        self.fields = {}
        self.parse(fields_str)

    def parse(self, str):
        labels = ['on_under', 'kind_hwy', 'levl_srvc', 'routenum',
                   'dirsuffix', 'district', 'county', 'placecode', 'featint', 'crit_feat',
                   'facility', 'location', 'vclrinv', 'kmpost', 'onbasenet', 'lrsinvrt', 'subrtnum',
                   'latitude', 'longitude', 'bypasslen', 'tollfac', 'custodian', 'owner', 'funcclass',
                   'yearbuilt', 'lanes_on', 'lanes_under', 'adttotal', 'adtyear', 'designload',
                   'aroadwidth', 'bridgemed', 'skew', 'strflared', 'railrating', 'transratin',
                   'arailratin', 'aendrating', 'histsign', 'navcntrol', 'navvc', 'navhc', 'oppostcl',
                   'servtypon', 'servtypund', 'material_main', 'design_main', 'material_appr',
                   'design_appr', 'mainspans', 'appspans', 'hclrinv', 'maxspan', 'length', 'lftcurbsw',
                   'rtcurbsw', 'roadwidth', 'deckwidth', 'vclrover', 'refvuc', 'vclrunder', 'refhuc',
                   'hclrurt', 'hclrult', 'dkrating', 'suprating', 'subrating', 'chanprot', 'culvrating',
                   'ortype', 'orload', 'irtype', 'irload', 'strrating', 'deckgeom', 'underclr',
                   'posting', 'wateradeq', 'appralign', 'propwork', 'workby', 'implen', 'inspdate',
                   'nbinspfreq', 'fraccrit', 'fcinspfreq', 'uwinspreq', 'uwinspfreq', 'osinspreq',
                   'osinspfreq', 'fclastinsp', 'uwlastinsp', 'oslastinsp', 'nbiimpcost', 'nbirwcost',
                   'nbitotcost', 'nbiyrcost', 'nstatecode', 'n_fhwa_reg', 'bb_pct', 'bb_brdgeid',
                   'defhwy', 'paralstruc', 'trafficdir', 'tempstruc', 'nhs_ind', 'fedlandhwy',
                   'yearrecon', 'dkstructyp', 'dksurftype', 'dkmembtype', 'dkprotect', 'truckpct',
                   'trucknet', 'pierprot', 'nbislen', 'scourcrit', 'adtfuture', 'adtfutyear',
                   'lftbrnavcl', 'reserved', 'nbi_rating', 'suff_prefx', 'suff_rate']

        self.full_bridge_id, rest_of_fields = str.split('\t', 1)
        self.full_bridge_id = self.full_bridge_id.rstrip()
        list_fields = rest_of_fields.split(',')
        for index in range(len(labels)):
            # trim fields or not???????
            self.fields[labels[index]] = list_fields[index].lstrip().rstrip() if len(list_fields) > index else ''

    def toString(self):
        return self.full_bridge_id + '\t' + ','.join(self.fields.values())

class NBIAS_brinsp_table:

    def __init__(self):
        self.dic = {}
        self.brinsp_columns = ['district', 'county',  'funcclass', 'yearbuilt', 'lanes_on', 'lanes_under', 'designload', 'strflared',
            'railrating', 'transratin', 'arailratin', 'aendrating', 'histsign', 'oppostcl', 'servtypon', 'servtypund', 'material_main',
            'design_main', 'material_appr', 'design_appr', 'mainspans', 'appspans', 'skew', 'hclrinv', 'length', 'lftcurbsw', 'rtcurbsw',
            'deckwidth', 'vclrover', 'refvuc', 'vclrunder', 'refhuc', 'hclrurt', 'hclrult', 'dkrating', 'suprating', 'subrating',
            'chanprot', 'culvrating', 'orload', 'irload', 'strrating', 'deckgeom', 'underclr', 'posting', 'wateradeq', 'appralign',
            'propwork', 'inspdate', 'fraccrit', 'nbiimpcost', 'nbiyrcost', 'nstatecode', 'n_fhwa_reg', 'defhwy', 'paralstruc',
            'tempstruc', 'nhs_ind', 'yearrecon', 'dkstructyp', 'dksurftype', 'dkmembtype', 'dkprotect', 'truckpct', 'pierprot',
            'nbislen', 'nbi_rating', 'suff_prefx', 'suff_rate', 'bridgemed', 'climate_zone', 'isbridge',
            # 'bad_data'
            'yearlast', 'rangelast',
            'fips_state', 'fhwa_regn', 'bridge_id', 'd_length', 'd_deckwidth', 'd_deckarea',
            # 'b_screened', 'f_mainspans', 'f_appspans'
            'state_brkey', 'state_reg'
            # 'traceflag'
            ]

        self.data_metric_row_columns = ['district',	'county', 'funcclass','yearbuilt', 'lanes_on', 'lanes_under', 'designload',
            'strflared','railrating','transratin','arailratin','aendrating','histsign','oppostcl','servtypon', 'servtypund',
            'material_main', 'design_main', 'material_appr', 'design_appr', 'mainspans', 'appspans', 'skew', 'hclrinv', 'length',
            'lftcurbsw', 'rtcurbsw', 'deckwidth', 'vclrover', 'refvuc', 'vclrunder', 'refhuc', 'hclrurt', 'hclrult', 'dkrating',
            'suprating', 'subrating', 'chanprot', 'culvrating', 'orload', 'irload', 'strrating', 'deckgeom', 'underclr', 'posting',
            'wateradeq', 'appralign', 'propwork', 'inspdate', 'fraccrit', 'nbiimpcost', 'nbiyrcost', 'nstatecode', 'n_fhwa_reg',
            'defhwy', 'paralstruc', 'tempstruc', 'nhs_ind', 'yearrecon', 'dkstructyp', 'dksurftype', 'dkmembtype', 'dkprotect',
            'truckpct', 'pierprot', 'nbislen', 'nbi_rating', 'suff_prefx', 'suff_rate', 'bridgemed']

        self.data_metric_req_columns = ['on_under', 'kind_hwy', 'levl_srvc', 'vclrinv', 'bypasslen', 'adttotal', 'adtyear',
            'aroadwidth', 'roadwidth', 'trafficdir', 'adtfuture', 'adtfutyear']

    def insert(self, data_metric):
        on_under = data_metric.fields['on_under'] if data_metric.fields.has_key('on_under') else ''

        if on_under == '1':
            if not self.dic.has_key(data_metric.full_bridge_id):
                self.dic[data_metric.full_bridge_id] = {}
                record = self.dic[data_metric.full_bridge_id]
                for label in self.data_metric_row_columns:
                    record[label] = data_metric.fields[label]
                for label in self.data_metric_req_columns:
                    record[label] = data_metric.fields[label]
            else:
                record = data_metric.fields
                max_record = self.dic[data_metric.full_bridge_id]
                for label in self.data_metric_row_columns:
                    max_record[label] = max(max_record[label], record[label])

    def update_all_columns(self):
        county_climate = NBIAS_county_climate()

        for bridge_id in self.dic.keys():
            if self.dic[bridge_id]['funcclass'] not in ['01','02','06','07','08','09','11','12','14','16','17','19']:
                self.dic[bridge_id]['funcclass'] = '9'

            value = self.dic[bridge_id]['yearbuilt']
            if self.search_pattern('[^0-9]', value) or len(value) <= 0 or int(value) < 1492:
                    self.dic[bridge_id]['yearbuilt'] = '1960'

            value = self.dic[bridge_id]['yearrecon']
            if self.search_pattern('[^0-9]', value) or len(value) <= 0 or int(value) < 1492:
                self.dic[bridge_id]['yearrecon'] = '0000'

            value = self.dic[bridge_id]['lanes_on']
            if self.search_pattern('[^0-9]', value) or len(value) <= 0:
                self.dic[bridge_id]['lanes_on'] = '02'

            value = self.dic[bridge_id]['lanes_under']
            if not self.search_pattern('[^0-9]', value) or len(value) <= 0:
                self.dic[bridge_id]['lanes_under'] = '00'

            if not self.match_pattern('^[0-9]$', self.dic[bridge_id]['designload']):
                self.dic[bridge_id]['designload'] = '5'

            for field in ['strflared', 'nhs_ind']:
                if not self.match_pattern('^[01]$', self.dic[bridge_id][field]):
                    self.dic[bridge_id][field] = '0'

            for field in ['railrating', 'transratin', 'arailratin', 'aendrating', 'chanprot', 'culvrating'] :
                if not self.match_pattern('^[0-9N]$', self.dic[bridge_id][field]):
                    self.dic[bridge_id][field] = 'N'

            if not self.match_pattern('^[1-5]$', self.dic[bridge_id]['histsign']):
                self.dic[bridge_id]['histsign'] = '5'

            if not self.match_pattern('^[ABDEGKPR]$', self.dic[bridge_id]['oppostcl']):
                self.dic[bridge_id]['oppostcl'] = 'A'

            for field in ['servtypon', 'material_main', 'material_appr']:
                if not self.match_pattern('^[0-9]$', self.dic[bridge_id][field]):
                    self.dic[bridge_id][field] = '1'

            if not self.match_pattern('^[0-9]$', self.dic[bridge_id]['servtypund']):
                self.dic[bridge_id]['servtypund'] = '5'

            for field in ['design_main', 'design_appr']:
                if not self.match_pattern('^[0-2][0-9]$', self.dic[bridge_id][field]):
                    self.dic[bridge_id][field] = '02'

            value = self.dic[bridge_id]['mainspans']
            if self.search_pattern('[^0-9]', value) or len(value) <= 0:
                self.dic[bridge_id]['mainspans'] = '001'

            for field in ['appspans', 'nbiyrcost']:
                value = self.dic[bridge_id][field]
                if self.search_pattern('[^0-9]', value) or len(value) <= 0:
                    self.dic[bridge_id][field] = '0000'

            value = self.dic[bridge_id]['skew']
            if self.search_pattern('[^0-9]', value) or len(value) <= 0:
                self.dic[bridge_id]['skew'] = '00'

            value = self.dic[bridge_id]['hclrinv']
            if self.search_pattern('[^0-9]', value) or len(value) <= 0:
                self.dic[bridge_id]['hclrinv'] = '073'

            value = self.dic[bridge_id]['length']
            if self.search_pattern('[^0-9]', value) or len(value) <= 1 or int(value) < 10:
                self.dic[bridge_id]['length'] = '000800'

            for field in ['lftcurbsw', 'rtcurbsw']:
                value = self.dic[bridge_id][field]
                if self.search_pattern('[^0-9]', value) or len(value) <= 0:
                    self.dic[bridge_id][field] = '006'

            value = self.dic[bridge_id]['deckwidth']
            if self.search_pattern('[^0-9]', value) or len(value) <= 1 or int(value) < 10:
                self.dic[bridge_id]['deckwidth'] = '0149'

            value = self.dic[bridge_id]['vclrover']
            if self.search_pattern('[^0-9]', value) or len(value) <= 0:
                self.dic[bridge_id]['vclrover'] = '9999'

            value = self.dic[bridge_id]['vclrunder']
            if self.search_pattern('[^0-9]', value) or len(value) <= 0:
                self.dic[bridge_id]['vclrunder'] = '0506'

            for field in ['refvuc', 'refhuc']:
                if not self.match_pattern('^[HRN]$', self.dic[bridge_id][field]):
                    self.dic[bridge_id][field] = 'N'

            value = self.dic[bridge_id]['hclrurt']
            if self.search_pattern('[^0-9]', value) or len(value) <= 0:
                self.dic[bridge_id]['hclrurt'] = '999'

            value = self.dic[bridge_id]['hclrult']
            if self.search_pattern('[^0-9]', value) or len(value) <= 0:
                self.dic[bridge_id]['hclrult'] = '000'

            for field in ['dkrating', 'suprating', 'subrating'] :
                if not self.match_pattern('^[0-9N]$', self.dic[bridge_id][field]):
                    self.dic[bridge_id][field] = '7'

            value = self.dic[bridge_id]['orload']
            if self.search_pattern('[^0-9]', value) or len(value) <= 0:
                self.dic[bridge_id]['orload'] = '601'

            value = self.dic[bridge_id]['irload']
            if self.search_pattern('[^0-9]', value) or len(value) <= 0:
                self.dic[bridge_id]['irload'] = '348'

            if not self.match_pattern('^[023456789N]$', self.dic[bridge_id]['strrating']):
                self.dic[bridge_id]['strrating'] = '6'

            if not self.match_pattern('^[023456789N]$', self.dic[bridge_id]['deckgeom']):
                self.dic[bridge_id]['deckgeom'] = '5'

            for field in ['underclr', 'wateradeq']:
                if not self.match_pattern('^[023456789N]$', self.dic[bridge_id][field]):
                    self.dic[bridge_id][field] = 'N'

            if not self.match_pattern('^[0-5]$', self.dic[bridge_id]['posting']):
                self.dic[bridge_id]['posting'] = 'N'

            for field in ['appralign', 'dksurftype']:
                if not self.match_pattern('^[0-9N]$', self.dic[bridge_id][field]):
                    self.dic[bridge_id][field] = '8'

            if not self.match_pattern('^[YN]$', self.dic[bridge_id]['fraccrit']):
                self.dic[bridge_id]['fraccrit'] = 'N'

            for field in ['defhwy', 'nbi_rating']:
                if not self.match_pattern('^[0-2]$', self.dic[bridge_id][field]):
                    self.dic[bridge_id][field] = '0'

            if not self.match_pattern('^[RLN]$', self.dic[bridge_id]['paralstruc']):
                self.dic[bridge_id]['paralstruc'] = 'N'

            if not self.match_pattern('^[Tt]$', self.dic[bridge_id]['tempstruc']):
                self.dic[bridge_id]['tempstruc'] = ' '
            else:
                self.dic[bridge_id]['tempstruc'] = 'T'

            if not self.match_pattern('^[1-9N]$', self.dic[bridge_id]['dkstructyp']):
                self.dic[bridge_id]['dkstructyp'] = '1'

            if not self.match_pattern('^[012389N]$', self.dic[bridge_id]['dkmembtype']):
                self.dic[bridge_id]['dkmembtype'] = '1'

            if not self.match_pattern('^[0-9N]$', self.dic[bridge_id]['dkprotect']):
                self.dic[bridge_id]['dkprotect'] = '9'

            value = self.dic[bridge_id]['truckpct']
            if self.search_pattern('[^0-9]', value) or len(value) <= 0:
                self.dic[bridge_id]['truckpct'] = '05'

            if not self.match_pattern('^[1-5]$', self.dic[bridge_id]['pierprot']):
                self.dic[bridge_id]['pierprot'] = '1'

            if not self.match_pattern('^[YN]$', self.dic[bridge_id]['nbislen']):
                self.dic[bridge_id]['nbislen'] = 'Y'

            value = self.dic[bridge_id]['suff_rate']
            if self.search_pattern('[^0-9]', value) or len(value) <= 0:
                self.dic[bridge_id]['suff_rate'] = '0810'

            if self.dic[bridge_id]['suff_prefx'] != '*':
                self.dic[bridge_id]['suff_prefx'] = ' '

            value = self.dic[bridge_id]['propwork']
            if self.search_pattern('[^0-9]', value) or len(value) <= 0:
                self.dic[bridge_id]['propwork'] = '31'

                value = self.dic[bridge_id]['nbiimpcost']
                if self.search_pattern('[^0-9]', value) or len(value) <= 0:
                    self.dic[bridge_id]['nbiimpcost'] = '000000'

            value = self.dic[bridge_id]['bridgemed']
            if self.search_pattern('[^0-9]', value) or len(value) <= 0:
                self.dic[bridge_id]['bridgemed'] = '0'

            currentyear = datetime.now().year
            yearcorn = int(self.dic[bridge_id]['yearrecon'])
            yearbuilt = int(self.dic[bridge_id]['yearbuilt'])
            if yearbuilt < yearcorn <= currentyear:
                yearlast = yearcorn
            else:
                yearlast = yearbuilt
            self.dic[bridge_id]['yearlast'] = str(yearlast)

            yearlast = self.dic[bridge_id]['yearlast']
            if yearlast < 1900:
                rangelast = 0
            else:
                rangelast = (int(yearlast) - 1900) / 5
            self.dic[bridge_id]['rangelast'] = str(rangelast)

            if self.dic[bridge_id]['culvrating'] != 'N' or (self.dic[bridge_id]['dkrating'] == 'N' and self.dic[bridge_id]['suprating'] == 'N' and self.dic[bridge_id]['subrating'] == 'N') or self.dic[bridge_id]['design_main'] == '18':
                isbridge = 'C'
            elif self.dic[bridge_id]['design_main'] == '19':
                isbridge = 'T'
            else:
                isbridge = 'B'
            self.dic[bridge_id]['isbridge'] = isbridge

            self.dic[bridge_id]['fips_state'] = bridge_id[:2]
            self.dic[bridge_id]['fhwa_regn'] = bridge_id[2:3]
            self.dic[bridge_id]['bridge_id'] = bridge_id[3:18]
            self.dic[bridge_id]['state_brkey'] = bridge_id.lstrip().rstrip()
            self.dic[bridge_id]['state_reg'] = bridge_id[:3]
            self.dic[bridge_id]['d_length'] = str(float(self.dic[bridge_id]['length']) / 10.0)
            self.dic[bridge_id]['d_deckwidth'] = str(float(self.dic[bridge_id]['deckwidth']) / 10.0)
            self.dic[bridge_id]['d_deckarea'] = str(float(self.dic[bridge_id]['length']) * float(self.dic[bridge_id]['deckwidth']) / 100.0)
            self.dic[bridge_id]['climate_zone'] = county_climate.get_climate_zone(self.dic[bridge_id]['fhwa_regn'], self.dic[bridge_id]['fips_state'], self.dic[bridge_id]['county'])

    def search_pattern(self, pattern, value):
        result = True
        m = re.search(pattern, value)
        if m is None:
            result = False
        return result

    def match_pattern(self, pattern, value):
        result = True
        m = re.match(pattern, value)
        if m is None:
            result = False
        return result

    def toString(self):
        result = ''
        for key in self.dic.keys():
            values = []
            for f in self.brinsp_columns:
                values.append(self.dic[key][f])
            result += 'br' + key + '\t' + ','.join(values) + '\n'

            for f in self.data_metric_req_columns:
                values.append(self.dic[key][f])
            result += 'dm' + key + '\t' + ','.join(values) + '\n'
        return result

# combine two tables nbias_state_county_climate and nbias_state_default_climate
class NBIAS_county_climate:
    def __init__(self):
        self.regions = []
        self.load()

    def get_climate_zone(self, region, state, county):
        result = 5

        if region != '' and state != '' and county != '':
            region = int(region)
            state = int(state)
            county = int(county)
            if region < len(self.regions) and self.regions[region].has_key(state) and self.regions[region][state] != {}:
                result = self.regions[region][state].get_climate_zone(county)
        elif state != '':
            state = int(state)
            for region in self.regions:
                if region.has_key(state):
                    result = region[state].get_climate_zone(county)

        return str(result)

    def load(self):
        # regions 0 - 9
        self.regions.append({16:{}, 41:{}, 53:{}})
        self.regions.append({9:{}, 23:{}, 25:{}, 33:{}, 44:{}, 50:{}})
        self.regions.append({34:{}, 36:{}})
        self.regions.append({10:{}, 11:{}, 24:{}, 42:{}, 51:{}, 54:{}})
        self.regions.append({1:{}, 12:{}, 13:{}, 21:{}, 28:{}, 37:{}, 45:{}, 47:{}})
        self.regions.append({17:{}, 18:{}, 26:{}, 27:{}, 39:{}, 55:{}})
        self.regions.append({5:{}, 22:{}, 35:{}, 40:{}, 48:{}})
        self.regions.append({19:{}, 20:{}, 29:{}, 31:{}})
        self.regions.append({8:{}, 30:{}, 38:{}, 46:{}, 49:{}, 56:{}})
        self.regions.append({4:{}, 6:{}, 15:{}, 32:{}})

        for region in self.regions:
            for state in region.keys():
                if state == 1:
                    non_default_county_climate = {33:2, 75:2, 77:2, 79:2, 83:2, 93:2}
                    region[state] = State(3, non_default_county_climate)
                elif state == 4:
                    non_default_county_climate = {1:7, 5:7, 7:5, 9:8, 11:5, 15:8, 17:7, 25:5}
                    region[state] = State(9, non_default_county_climate)
                elif state == 5:
                    non_default_county_climate = {27:3, 73:3}
                    region[state] = State(2, non_default_county_climate)
                elif state == 6:
                    non_default_county_climate = {1:9, 3:4, 5:4, 7:4, 9:4, 11:6, 13:9, 15:3, 17:4, 19:4, 21:6, 23:3, 25:9, 27:8, 29:6, 31:6, 33:3, 35:4, 37:6, 39:4, 41:6, 43:4, 45:3, 47:6, 49:4, 53:6, 55:6, 57:4, 59:9, 61:4, 63:4, 65:9, 67:6, 69:9, 71:9, 73:6, 75:6, 77:9, 79:6, 81:6, 83:6, 85:9, 87:6, 89:2, 91:4, 93:3, 95:9, 97:3, 99:9, 101:6, 103:3, 105:3, 107:5, 109:4, 111:6, 113:9, 115:4}
                    region[state] = State(7, non_default_county_climate)
                elif state == 8:
                    non_default_county_climate = {1:7, 5:7, 9:7, 11:7, 15:1, 17:7, 19:1, 21:1, 25:7, 39:7, 61:7, 63:7, 65:1, 69:7, 73:7, 75:7, 77:7, 79:1, 83:7, 85:7, 87:7, 89:7, 93:1, 95:7, 99:7, 105:1, 109:1, 115:7, 117:1, 121:7, 123:7, 125:7}
                    region[state] = State(4, non_default_county_climate)
                elif state == 12 or state == 13 or state == 15:
                    region[state] = State(3)
                elif state == 16:
                    non_default_county_climate = {1:7, 11:7, 17:1, 21:1, 23:7, 27:7, 31:7, 35:1, 37:1, 39:7, 47:7, 49:1, 51:7, 53:7, 55:1, 59:1, 63:7, 67:7, 73:7, 75:7, 77:7, 79:1, 83:7}
                    region[state] = State(4, non_default_county_climate)
                elif state == 17:
                    non_default_county_climate = {3:2, 23:2, 25:2, 33:2, 45:2, 47:2, 55:2, 59:2, 65:2, 69:2, 77:2, 79:2, 81:2, 87:2, 101:2, 127:2, 133:2, 145:2, 151:2, 153:2, 157:2, 159:2, 165:2, 181:2, 185:2, 189:2, 191:2, 193:2, 199:2}
                    region[state] = State(1, non_default_county_climate)
                elif state == 18:
                    non_default_county_climate = {19:2, 21:2, 25:2, 27:2, 29:2, 37:2, 43:2, 51:2, 55:2, 61:2, 71:2, 77:2, 79:2, 83:2, 93:2, 101:2, 115:2, 117:2, 119:2, 121:2, 123:2, 125:2, 129:2, 137:2, 143:2, 147:2, 153:2, 155:2, 163:2, 165:2, 167:2, 173:2, 175:2}
                    region[state] = State(1, non_default_county_climate)
                elif state == 19:
                    non_default_county_climate = {5:1, 7:1, 11:1, 13:1, 17:1, 19:1, 31:1, 37:1, 43:1, 45:1, 51:1, 55:1, 57:1, 61:1, 65:1, 87:1, 89:1, 95:1, 97:1, 101:1, 103:1, 105:1, 107:1, 111:1, 113:1, 115:1, 117:1, 123:1, 125:1, 135:1, 139:1, 157:1, 163:1, 177:1, 179:1, 183:1, 185:1, 191:1}
                    region[state] = State(4, non_default_county_climate)
                elif state == 20:
                    non_default_county_climate = {1:5, 11:2, 19:5, 21:2, 23:7, 25:7, 33:7, 37:2, 39:7, 47:7, 49:5, 51:7, 55:7, 57:7, 63:7, 65:7, 67:7, 69:7, 71:7, 75:7, 81:7, 83:7, 93:7, 97:7, 99:5, 101:7, 107:1, 109:7, 119:7, 125:5, 129:7, 133:5, 135:7, 137:7, 145:7, 147:7, 153:7, 163:7, 165:7, 171:7, 175:7, 179:7, 181:7, 187:7, 189:7, 193:7, 195:7, 199:7, 203:7, 205:5, 207:5}
                    region[state] = State(4, non_default_county_climate)
                elif state == 22:
                    non_default_county_climate = {67:2, 111:2}
                    region[state] = State(3, non_default_county_climate)
                elif state == 10 or state == 11 or state == 21 or state == 24:
                    region[state] = State(2)
                elif state == 27:
                    non_default_county_climate = {17:1, 31:1, 45:1, 55:1, 75:1, 109:1, 115:1, 137:1, 157:1, 169:1}
                    region[state] = State(4, non_default_county_climate)
                elif state == 28:
                    non_default_county_climate = {3:2, 9:2, 17:2, 25:2, 33:2, 57:2, 81:2, 93:2, 95:2, 115:2, 117:2, 137:2, 139:2, 141:2, 143:2, 145:2}
                    region[state] = State(3, non_default_county_climate)
                elif state == 29:
                    non_default_county_climate = {1:1, 3:4, 5:4, 7:1, 13:1, 15:1, 19:1, 21:4, 25:1, 27:1, 33:1, 37:1, 41:1, 45:1, 47:4, 49:4, 51:1, 53:1, 61:4, 63:4, 71:1, 73:1, 75:4, 79:1, 81:4, 83:1, 87:4, 89:1, 95:1, 101:1, 103:1, 107:1, 111:1, 113:1, 115:1, 117:1, 121:1, 127:1, 129:1, 135:1, 137:1, 139:1, 141:1, 147:4, 151:1, 159:1, 163:1, 165:4, 171:1, 173:1, 175:1, 177:1, 183:1, 189:1, 195:1, 197:1, 199:1, 205:1, 211:1, 219:1, 227:4, 510:1}
                    region[state] = State(2, non_default_county_climate)
                elif state == 30:
                    non_default_county_climate = {3:7, 5:7, 11:7, 15:7, 17:7, 19:7, 21:7, 25:7, 27:7, 33:7, 37:7, 41:7, 51:7, 53:1, 55:7, 61:1, 65:7, 67:1, 69:7, 71:7, 73:7, 75:7, 79:7, 83:7, 85:7, 87:7, 89:1, 91:7, 101:7, 103:7, 105:7, 109:7, 111:7}
                    region[state] = State(4, non_default_county_climate)
                elif state == 31:
                    non_default_county_climate = {7:7, 9:7, 13:7, 17:7, 29:7, 31:7, 33:7, 41:7, 45:7, 47:7, 49:7, 57:7, 63:7, 65:7, 69:7, 73:7, 83:7, 85:7, 87:7, 91:7, 101:7, 103:7, 105:7, 111:7, 113:7, 123:7, 135:7, 145:7, 157:7, 161:7, 165:7, 171:7}
                    region[state] = State(4, non_default_county_climate)
                elif state == 32:
                    non_default_county_climate = {3:8}
                    region[state] = State(7, non_default_county_climate)
                elif state == 34:
                    non_default_county_climate = {1:2, 5:2, 7:2, 9:2, 11:2, 15:2, 21:2, 23:2, 25:2, 29:2, 33:2}
                    region[state] = State(1, non_default_county_climate)
                elif state == 35:
                    non_default_county_climate = {7:4, 23:9, 28:4, 31:7, 33:4, 39:4, 45:7, 47:4, 49:4, 55:4}
                    region[state] = State(8, non_default_county_climate)
                elif state == 37:
                    non_default_county_climate = {1:2, 3:2, 5:2, 9:2, 25:2, 27:2, 33:2, 35:2, 37:2, 45:2, 57:2, 59:2, 63:2, 65:2, 67:2, 69:2, 71:2, 77:2, 81:2, 83:2, 85:2, 97:2, 101:2, 105:2, 109:2, 119:2, 127:2, 131:2, 135:2, 145:2, 151:2, 157:2, 159:2, 169:2, 171:2, 181:2, 183:2, 185:2, 189:2, 193:2, 195:2, 197:2}
                    region[state] = State(3, non_default_county_climate)
                elif state == 38:
                    non_default_county_climate = {1:7, 7:7, 9:7, 11:7, 13:7, 15:7, 23:7, 25:7, 29:7, 33:7, 37:7, 41:7, 49:7, 53:7, 55:7, 57:7, 59:7, 61:7, 65:7, 75:7, 83:7, 85:7, 87:7, 89:7, 101:7, 105:7}
                    region[state] = State(4, non_default_county_climate)
                elif state == 39:
                    non_default_county_climate = {1:2, 9:2, 13:2, 15:2, 25:2, 27:2, 45:2, 47:2, 53:2, 59:2, 61:2, 71:2, 73:2, 79:2, 87:2, 105:2, 111:2, 115:2, 119:2, 121:2, 127:2, 129:2, 131:2, 141:2, 145:2, 163:2, 165:2, 167:2}
                    region[state] = State(1, non_default_county_climate)
                elif state == 40:
                    non_default_county_climate = {1:2, 3:4, 7:7, 9:8, 21:2, 23:2, 25:7, 35:2, 39:8, 41:2, 43:8, 45:7, 53:4, 55:8, 57:8, 59:7, 61:2, 65:8, 75:8, 77:2, 79:2, 89:2, 97:2, 101:2, 115:2, 127:2, 129:8, 135:2, 139:7, 141:8, 145:2, 149:8, 151:7, 153:7}
                    region[state] = State(5, non_default_county_climate)
                elif state == 41:
                    non_default_county_climate = {1:4, 13:7, 17:4, 21:7, 23:4, 25:7, 27:2, 31:4, 35:4, 37:7, 45:7, 49:7, 55:4, 59:4, 61:1, 63:1, 65:4, 69:4}
                    region[state] = State(3, non_default_county_climate)
                elif state == 42:
                    non_default_county_climate = {45:2, 51:2, 59:2, 101:2, 125:2}
                    region[state] = State(1, non_default_county_climate)
                elif state == 45:
                    non_default_county_climate = {21:2}
                    region[state] = State(3, non_default_county_climate)
                elif state == 46:
                    non_default_county_climate = {7:7, 17:7, 19:1, 21:7, 31:7, 33:1, 41:7, 47:1, 55:7, 63:7, 65:7, 69:7, 71:7, 75:7, 81:1, 85:7, 93:7, 95:7, 103:7, 105:7, 107:7, 113:7, 117:7, 119:7, 121:7, 123:7, 129:7, 137:7}
                    region[state] = State(4, non_default_county_climate)
                elif state == 47:
                    non_default_county_climate = {11:3, 51:3, 65:3, 115:3, 139:3, 171:3}
                    region[state] = State(2, non_default_county_climate)
                elif state == 48:
                    non_default_county_climate = {1:6, 5:3, 7:9, 13:9, 15:6, 19:9, 21:6, 25:9, 27:6, 29:9, 31:9, 35:6, 37:2, 39:6, 41:6, 47:9, 51:6, 53:9, 55:6, 57:6, 61:9, 63:3, 67:3, 71:3, 73:6, 77:5, 85:5, 89:6, 91:9, 97:5, 99:6, 113:6, 121:5, 123:6, 127:9, 131:9, 137:9, 139:6, 145:6, 147:5, 149:6, 157:6, 159:3, 161:6, 163:9, 167:6, 171:9, 175:9, 177:6, 181:5, 183:3, 185:6, 187:9, 193:6, 199:3, 201:6, 203:3, 209:9, 213:6, 215:9, 217:6, 221:5, 223:6, 225:6, 231:6, 237:5, 239:6, 241:3, 245:3, 247:9, 249:9, 251:6, 255:9, 257:6, 259:9, 261:9, 265:9, 267:9, 271:9, 273:9, 277:2, 281:9, 283:9, 285:6, 287:6, 289:6, 291:3, 293:6, 295:7, 297:9, 299:9, 307:9, 309:6, 311:9, 313:6, 315:3, 319:9, 321:6, 323:9, 325:9, 327:9, 331:6, 333:9, 337:5, 339:6, 343:3, 347:3, 349:6, 351:3, 355:9, 361:3, 365:3, 367:5, 373:3, 379:6, 385:9, 387:2, 391:9, 395:6, 397:6, 401:3, 403:3, 405:3, 407:3, 409:9, 411:9, 419:3, 423:6, 425:6, 427:9, 435:9, 439:5, 443:9, 449:3, 453:6, 455:3, 457:3, 459:3, 463:9, 465:9, 467:6, 469:6, 471:6, 473:6, 477:6, 479:9, 481:6, 485:5, 489:9, 491:6, 493:9, 497:5, 499:6, 505:9, 507:9}
                    region[state] = State(8, non_default_county_climate)
                elif state == 49:
                    non_default_county_climate = {5:4, 9:4, 13:4, 29:4, 33:4, 35:4, 39:4, 43:4, 49:4, 51:4, 53:8, 57:4}
                    region[state] = State(7, non_default_county_climate)
                elif state == 9 or state == 23 or state == 25 or state == 26 or state == 33 or state == 36 or state == 44 or state == 50 or state == 55:
                    region[state] = State(1)
                elif state == 51:
                    non_default_county_climate = {93:3, 199:3, 650:3, 700:3, 710:3, 740:3, 800:3, 810:3}
                    region[state] = State(2, non_default_county_climate)
                elif state == 53:
                    non_default_county_climate = {5:7, 9:3, 11:3, 15:3, 17:7, 21:7, 25:7, 27:3, 31:3, 33:3, 35:3, 41:3, 45:3, 47:7, 49:3, 53:3, 57:2, 59:3, 61:3, 67:3, 69:3, 73:2}
                    region[state] = State(4, non_default_county_climate)
                elif state == 54:
                    non_default_county_climate = {29:1}
                    region[state] = State(2, non_default_county_climate)
                elif state == 56:
                    non_default_county_climate = {3:1, 11:1, 13:4, 17:4, 23:4, 29:4, 33:1, 35:4, 39:1, 41:4, 43:1, 45:1}
                    region[state] = State(7, non_default_county_climate)

class State:
    def __init__(self, default_climate_zone, non_default_county_climate = {}):
        self.default_climate_zone = default_climate_zone
        self.non_default_county_climate = non_default_county_climate

    def get_climate_zone(self, county):
        result = self.default_climate_zone
        if self.non_default_county_climate.has_key(county):
            result = self.non_default_county_climate[county]
        return result

def main(argv):
    # mapperr_max_raw_data_output = open('data/1_mapper_brinsp_data_metric_output.txt', 'r')
    # lines = mapperr_max_raw_data_output.readlines()
    # reducerr_max_raw_data_output = open('data/1_reducer_brinsp_data_metric_output.txt', 'w')
    nbias_brinsp = NBIAS_brinsp_table()

    # for line in lines:
    for line in sys.stdin:
        line = line.rstrip()
        bridge_data = NBIAS_data_metric_row(line)
        nbias_brinsp.insert(bridge_data)

    nbias_brinsp.update_all_columns()

    print nbias_brinsp.toString()
    # reducerr_max_raw_data_output.write(nbias_brinsp.toString())
    #
    # mapperr_max_raw_data_output.close()
    # reducerr_max_raw_data_output.close()

if __name__ == '__main__':
    main(sys.argv)
