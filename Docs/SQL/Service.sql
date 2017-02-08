select ROW_NUMBER() over (order by AgreementId desc ) rown , Value 
into temp
from  SeviceAgreement

update sag set sag.AgreementId = t.rown
from SeviceAgreement sag
inner join temp t on sag.Value = t.Value